// <copyright file="NgJsContext.cs" company="Sintra">
// Copyright (c) Sintra. All rights reserved.
// </copyright>

namespace UmbracoAngularJs.Context
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Web;
    using UmbracoAngularJs.Classes;
    using UmbracoAngularJs.Helpers;
    using UmbracoAngularJs.Providers;

    /// <summary>
    /// Inclusion mode for AngularJS files.
    /// </summary>
    public enum IncludeMode
    {
        Link,
        Load
    }

    /// <summary>
    /// Context for AngularJS related tasks.
    /// </summary>
    public class NgJsContext
    {
        private static readonly string HttpContextKey = "ngJsContext";

        /// <summary>
        /// Gets or sets the provider factory.
        /// </summary>
        /// <value>
        /// The provider factory.
        /// </value>
        public static Func<INgJsProvider> ProviderFactory { get; set; } =
            new Func<INgJsProvider>(() => new DefaultNgJsProvider());

        /// <summary>
        /// Gets the current context. If not already present, a default one will be created.
        /// </summary>
        /// <value>
        /// The current context.
        /// </value>
        public static NgJsContext Current
        {
            get
            {
                NgJsContext current = HttpContext.Current.Items[HttpContextKey] as NgJsContext;

                if (current == null)
                {
                    current = new NgJsContext
                    {
                        IncludeMode = IncludeMode.Load,
                        Provider = ProviderFactory.Invoke()
                    };
                }

                return current;
            }
        }

        /// <summary>
        /// Gets or sets the inclusion mode for AngularJS files inside the page.
        /// </summary>
        /// <value>
        /// The inclusion mode.
        /// </value>
        public IncludeMode IncludeMode { get; set; }

        /// <summary>
        /// Gets or sets the AngularJS framework provider.
        /// </summary>
        /// <value>
        /// The AngularJS provider.
        /// </value>
        public INgJsProvider Provider { get; set; } = null;

        /// <summary>
        /// Creates and registers the AngularJS initialize script. To be called before
        /// <see cref="UmbracoAngularJs.Extensions.HtmlHelperExtension.RenderNgJsInit(System.Web.Mvc.HtmlHelper)"/>.
        /// </summary>
        public void RegisterNgJsInitScript()
        {
            string script = GenerateNgJsInitScript();
            NgJsViewDependenciesContext.Current.InitScript = script;
        }

        private string GenerateNgJsInitScript()
        {
            // Get app name
            string appName = Provider.GetAppName();

            // Gather specified dependencies
            Dictionary<string, NgJsViewDeps> dependencies = Provider.GetAllDependencies();

            // Gather all distinct AngularJS module names
            List<string> moduleNames = dependencies.Values.Select(d => d.Modules).Aggregate(
                new List<string>(), (total, next) => total.Concat(next).ToList())
                .Distinct().ToList();

            // Script to bootstrap app
            StringBuilder script = new StringBuilder();
            script.AppendLine(string.Empty);
            script.AppendFormat(
                " var app = angular.module({0},{1});",
                ScriptHelper.ConvertStringForJs(appName),
                ScriptHelper.GetJsStringArray(moduleNames));
            script.AppendLine(string.Empty);

            RegisterModulesConfig(script, moduleNames);
            RegisterServices(script, dependencies);
            RegisterDirectives(script, dependencies);
            RegisterComponents(script, dependencies);
            RegisterFilters(script, dependencies);

            AppendViews(script, dependencies);
            script.AppendLine(string.Empty);

            RegisterViewControllers(script, dependencies);
            RegisterScopeInit(script, dependencies);

            return script.ToString();
        }

        private void RegisterModulesConfig(StringBuilder script, List<string> moduleNames)
        {
            script.AppendLine();
            script.AppendLine("/** Register modules config **/");

            foreach (string module in moduleNames)
            {
                NgJsBaseData moduleConfig = Provider.GetConfigData(module);

                if (File.Exists(moduleConfig.Path))
                {
                    string jsCode = File.ReadAllText(NgJsHelper.MapPath(moduleConfig.Path)) + Environment.NewLine
                        + string.Format("app.config({0});", moduleConfig.JsName);
                    script.AppendLine(ScriptHelper.WrapJsCodeInIIFE(jsCode));
                }
            }

            script.AppendLine("/** End Register modules config **/");
            script.AppendLine();
        }

        private void RegisterServices(StringBuilder script, Dictionary<string, NgJsViewDeps> dependencies)
        {
            script.AppendLine();
            script.AppendLine("/** Register services **/");
            List<string> services = new List<string>();
            foreach (string s in dependencies.Keys)
            {
                NgJsViewDeps dep = dependencies[s];

                foreach (string service in dep.Services)
                {
                    if (service != null)
                    {
                        services.Add(service.Trim());
                    }
                }
            }

            // include services one time only
            foreach (string service in services.Distinct())
            {
                NgJsBaseData serviceData = Provider.GetServiceData(service);

                script.AppendLine();
                if (IncludeMode == IncludeMode.Load)
                {
                    string mappedPath = NgJsHelper.MapPath(serviceData.Path);
                    if (File.Exists(mappedPath))
                    {
                        script.Append(File.ReadAllText(mappedPath));
                    }
                }
                else
                {
                    script.AppendLine("Include mode: link not implemented");
                }

                script.AppendLine();
                script.AppendFormat("app.service('{1}', {0});", serviceData.NgName, serviceData.JsName);
            }

            script.AppendLine("/** End Register services **/");
            script.AppendLine();
        }

        private void RegisterDirectives(StringBuilder script, Dictionary<string, NgJsViewDeps> dependencies)
        {
            List<string> directives = dependencies.Values.Select(d => d.Directives).Aggregate(
                new List<string>(), (total, next) => total.Concat(next).ToList())
                .Distinct().ToList();

            script.AppendLine();
            script.AppendLine("/** Register directives: start **/");

            foreach (string directiveName in directives)
            {
                NgJsBaseData directiveData = Provider.GetDirectiveData(directiveName);
                string directiveJs = File.ReadAllText(NgJsHelper.MapPath(directiveData.Path));

                string directiveScript = string.Format(
                    "{0} ; app.directive('{1}', {2});",
                    directiveJs,
                    directiveData.NgName,
                    directiveData.JsName);

                script.Append(ScriptHelper.WrapJsCodeInIIFE(directiveScript));
                script.AppendLine();
            }

            script.AppendLine("/** Register directives: end **/");
        }

        private void RegisterComponents(StringBuilder script, Dictionary<string, NgJsViewDeps> dependencies)
        {
            List<string> components = dependencies.Values.Select(d => d.Components).Aggregate(
                new List<string>(), (total, next) => total.Concat(next).ToList())
                .Distinct().ToList();

            script.AppendLine();
            script.AppendLine("/** Register components: start **/");

            foreach (string componentName in components)
            {
                NgJsBaseData componentData = Provider.GetComponentData(componentName);
                string componentJs = File.ReadAllText(NgJsHelper.MapPath(componentData.Path));

                string componentScript = string.Format(
                    "{0} ; app.component('{1}', {2});",
                    componentJs,
                    componentData.NgName,
                    componentData.JsName);

                script.Append(ScriptHelper.WrapJsCodeInIIFE(componentScript));
                script.AppendLine();
            }

            script.AppendLine("/** Register components: end **/");
        }

        private void RegisterFilters(StringBuilder script, Dictionary<string, NgJsViewDeps> dependencies)
        {
            List<string> filters = dependencies.Values.Select(d => d.Filters).Aggregate(
                new List<string>(), (total, next) => total.Concat(next).ToList())
                .Distinct().ToList();

            script.AppendLine();
            script.AppendLine("/** Register filters: start **/");

            foreach (string filterName in filters)
            {
                NgJsBaseData filterData = Provider.GetFilterData(filterName);
                string filterJs = File.ReadAllText(NgJsHelper.MapPath(filterData.Path));

                string filterScript = string.Format(
                    "{0} ; app.filter('{1}', ['$sce', {2}]);",
                    filterJs,
                    filterData.NgName,
                    filterData.JsName);

                script.Append(ScriptHelper.WrapJsCodeInIIFE(filterScript));
                script.AppendLine();
            }

            script.AppendLine("/** Register filters: end **/");
        }

        private void AppendViews(StringBuilder script, Dictionary<string, NgJsViewDeps> deps)
        {
            foreach (string c in deps.Keys)
            {
                NgJsViewDeps dep = deps[c];

                if (!string.IsNullOrEmpty(dep.View))
                {
                    script.AppendLine();
                    if (IncludeMode == IncludeMode.Load)
                    {
                        script.AppendLine(dep.View);
                    }
                    else
                    {
                        script.AppendLine("/** TODO:modalità non implementata **/");
                    }

                    script.AppendLine();
                }
            }
        }

        private void RegisterViewControllers(StringBuilder script, Dictionary<string, NgJsViewDeps> dependencies)
        {
            script.AppendLine();
            script.AppendLine("/** Register view controllers **/");

            foreach (string c in dependencies.Keys)
            {
                NgJsViewDeps dep = dependencies[c];

                if (!string.IsNullOrEmpty(dep.View))
                {
                    NgJsViewData viewData = Provider.GetViewData(c);

                    script.AppendFormat("app.controller(\"{0}\", {0}); ", viewData.JsName);
                    script.AppendLine();
                }
            }

            script.AppendLine("/** End Register view controllers **/");
            script.AppendLine();
        }

        private void RegisterScopeInit(StringBuilder script, Dictionary<string, NgJsViewDeps> dependencies)
        {
            script.AppendLine();
            script.AppendLine("/** Register init functions **/");

            foreach (string viewName in dependencies.Keys)
            {
                NgJsViewData viewData = Provider.GetViewData(viewName);

                script.AppendFormat("if (typeof {0} == 'function') {{", viewData.InitFunctionJsName);
                script.AppendFormat("app.run({0});", viewData.InitFunctionJsName);
                script.AppendLine("}");
            }

            script.AppendLine("/** END Register init functions **/");
            script.AppendLine();
        }
    }
}