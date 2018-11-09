// <copyright file="HtmlHelperExtension.cs" company="Sintra">
// Copyright (c) Sintra. All rights reserved.
// </copyright>

namespace UmbracoAngularJs.Extensions
{
    using System;
    using System.IO;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;
    using UmbracoAngularJs.Classes;
    using UmbracoAngularJs.Context;
    using UmbracoAngularJs.Helpers;

    /// <summary>
    /// Extension methods for <see cref="HtmlHelper"/>
    /// </summary>
    public static class HtmlHelperExtension
    {
        /// <summary>
        /// Render the AngularJS init script.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <returns>The HTML string to write.</returns>
        public static IHtmlString NgJsInit(this HtmlHelper htmlHelper)
        {
            return htmlHelper.Raw(NgJsViewDependenciesContext.Current.InitScript);
        }

        /// <summary>
        /// Render a partial view with AngularJS components.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="viewName">The view name.</param>
        /// <returns>The HTML string to print.</returns>
        /// <seealso cref="PartialExtensions.Partial(HtmlHelper, string, object, ViewDataDictionary)"/>
        public static MvcHtmlString PartialNgJs(this HtmlHelper htmlHelper, string viewName)
        {
            return htmlHelper.PartialNgJs(viewName, null, null);
        }

        /// <summary>
        /// Render a partial view with AngularJS components.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="viewName">The view name.</param>
        /// <param name="model">The model.</param>
        /// <returns>The HTML string to print.</returns>
        /// <seealso cref="PartialExtensions.Partial(HtmlHelper, string, object, ViewDataDictionary)"/>
        public static MvcHtmlString PartialNgJs(this HtmlHelper htmlHelper, string viewName, object model)
        {
            return htmlHelper.PartialNgJs(viewName, model, null);
        }

        /// <summary>
        /// Render a partial view with AngularJS components.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="viewName">The view name.</param>
        /// <param name="viewDataDictionary">The view data dictionary.</param>
        /// <returns>The HTML string to print.</returns>
        /// <seealso cref="PartialExtensions.Partial(HtmlHelper, string, object, ViewDataDictionary)"/>
        public static MvcHtmlString PartialNgJs(
            this HtmlHelper htmlHelper,
            string viewName,
            ViewDataDictionary viewDataDictionary)
        {
            return htmlHelper.PartialNgJs(viewName, null, viewDataDictionary);
        }

        /// <summary>
        /// Render a partial view with AngularJS components.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="viewName">The view name.</param>
        /// <param name="model">The model.</param>
        /// <param name="viewDataDictionary">The view data dictionary.</param>
        /// <param name="ngJsDeps">The Angular JS dependencies related to this view.</param>
        /// <returns>The HTML string to print.</returns>
        /// <seealso cref="PartialExtensions.Partial(HtmlHelper, string, object, ViewDataDictionary)"/>
        public static MvcHtmlString PartialNgJs(
            this HtmlHelper htmlHelper,
            string viewName,
            object model,
            ViewDataDictionary viewDataDictionary,
            NgJsViewDeps ngJsDeps = null)
        {
            string sanitizedViewName = GetSanitizedNameFrom(viewName);
            NgJsViewData viewData = NgJsContext.Current.Provider.GetViewData(sanitizedViewName);

            NgJsViewDeps actualNgDeps = new NgJsViewDeps();
            NgJsViewDependenciesContext.Current.Dependencies.Add(sanitizedViewName, actualNgDeps);
            actualNgDeps.Merge(ngJsDeps);
            LoadView(actualNgDeps, viewData);

            string controllerName = viewData.JsName;
            string formName = sanitizedViewName + "Frm"; // FIXME: Use proper init from provider

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(@"<div ng-controller=""{0} as _context"" ng-form=""{1}"">", controllerName, formName);
            sb.AppendLine(htmlHelper.Partial(viewData.TemplatePath, model, viewDataDictionary).ToHtmlString());
            sb.AppendLine("</div>");
            return new MvcHtmlString(sb.ToString());
        }

        private static void LoadView(NgJsViewDeps target, NgJsViewData viewData)
        {
            if (target.View != null)
            {
                throw new ArgumentException("Unable to load " + viewData.JsName + ": view has already been loaded!");
            }

            string diskPath = NgJsHelper.MapPath(viewData.Path);
            target.View = "/** INCLUDE JS " + viewData.JsName + " @" + diskPath + "**/";

            if (File.Exists(diskPath))
            {
                target.View += File.ReadAllText(diskPath);
            }
            else
            {
                target.View = "/** js at '" + diskPath + "' not found **/";
            }
        }

        private static string GetSanitizedNameFrom(string viewName)
        {
            string res = Path.GetFileNameWithoutExtension(viewName);
            return res;
        }
    }
}