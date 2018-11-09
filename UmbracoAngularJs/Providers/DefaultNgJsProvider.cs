// <copyright file="DefaultNgJsProvider.cs" company="Sintra">
// Copyright (c) Sintra. All rights reserved.
// </copyright>

namespace UmbracoAngularJs.Providers
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web;
    using UmbracoAngularJs.Classes;
    using UmbracoAngularJs.Context;
    using UmbracoAngularJs.Extensions;
    using UmbracoAngularJs.Helpers;
    using UmbracoUtils.Extensions;

    /// <summary>
    /// Default implementation for <see cref="INgJsProvider"/>.
    /// </summary>
    /// <seealso cref="INgJsProvider" />
    public class DefaultNgJsProvider : INgJsProvider
    {
        private static readonly string CshtmlExt = ".cshtml";
        private static readonly string JsExt = ".js";
        private static readonly string DefaultConfigSuffix = "Config";
        private static readonly string DefaultViewSuffix = "View";
        private static readonly string DefaultServiceSuffix = "Service";
        private static readonly string DefaultComponentSuffix = "Component";
        private static readonly string DefaultDirectiveSuffix = "Directive";
        private static readonly string DefaultFilterSuffix = "Filter";

        private static readonly string DefaultControllerSuffix = "Controller";
        private static readonly string DefaultControllerInitSuffix = "Init";

        private string cachedViewsPath = null;
        private string cachedConfigsPath = null;
        private string cachedServicesPath = null;
        private string cachedDirectivesPath = null;
        private string cachedComponentsPath = null;
        private string cachedFiltersPath = null;

        private string ViewsPath
        {
            get
            {
                if (this.cachedViewsPath == null)
                {
                    this.cachedViewsPath = NgJsDefaultConventionHelper.GetViewsPath();
                }

                return this.cachedViewsPath;
            }
        }

        private string ConfigsPath
        {
            get
            {
                if (this.cachedConfigsPath == null)
                {
                    this.cachedConfigsPath = NgJsDefaultConventionHelper.GetConfigsPath();
                }

                return this.cachedConfigsPath;
            }
        }

        private string ServicesPath
        {
            get
            {
                if (this.cachedServicesPath == null)
                {
                    this.cachedServicesPath = NgJsDefaultConventionHelper.GetServicesPath();
                }

                return this.cachedServicesPath;
            }
        }

        private string ComponentsPath
        {
            get
            {
                if (this.cachedComponentsPath == null)
                {
                    this.cachedComponentsPath = NgJsDefaultConventionHelper.GetComponentsPath();
                }

                return this.cachedComponentsPath;
            }
        }

        private string FiltersPath
        {
            get
            {
                if (this.cachedFiltersPath == null)
                {
                    this.cachedFiltersPath = NgJsDefaultConventionHelper.GetFiltersPath();
                }

                return this.cachedFiltersPath;
            }
        }

        private string DirectivesPath
        {
            get
            {
                if (this.cachedDirectivesPath == null)
                {
                    this.cachedDirectivesPath = NgJsDefaultConventionHelper.GetDirectivesPath();
                }

                return this.cachedDirectivesPath;
            }
        }

        public string GetAppName()
        {
            var routeValues = HttpContext.Current.Request.RequestContext.RouteData.Values;

            string name = string.Empty;
            if (routeValues != null)
            {
                if (routeValues.ContainsKey("action"))
                {
                    name += routeValues["action"].ToString();
                }

                if (routeValues.ContainsKey("controller"))
                {
                    name += routeValues["controller"].ToString();
                }
            }

            if (name == string.Empty)
            {
                return "app";
            }

            return name.FirstCharToLower();
        }

        public NgJsBaseData GetConfigData(string moduleName)
        {
            string configName = string.Join(
                    string.Empty,
                    moduleName.Split(new char[] { '.', '-' }).Select(s => s.FirstCharToUpper()).ToList())
                    + DefaultConfigSuffix;

            return new NgJsBaseData
            {
                Path = NgJsHelper.GetNgJsFilePath(this.ConfigsPath, configName, JsExt),
                JsName = configName
            };
        }

        public NgJsViewData GetViewData(string viewName)
        {
            var baseDir = Path.Combine(this.ViewsPath, viewName);
            var baseFilename = viewName + DefaultViewSuffix;

            return new NgJsViewData
            {
                Path = NgJsHelper.GetNgJsFilePath(baseDir, baseFilename, JsExt),
                JsName = viewName + DefaultControllerSuffix,
                InitFunctionJsName = viewName + DefaultControllerInitSuffix,
                TemplatePath = NgJsHelper.GetNgJsFilePath(baseDir, baseFilename, CshtmlExt)
            };
        }

        public NgJsBaseData GetServiceData(string serviceName)
        {
            var basename = serviceName + DefaultServiceSuffix;

            return new NgJsBaseData
            {
                Path = NgJsHelper.GetNgJsFilePath(this.ServicesPath, basename, JsExt),
                JsName = basename
            };
        }

        public NgJsBaseData GetComponentData(string componentName)
        {
            var basename = componentName + DefaultComponentSuffix;

            return new NgJsBaseData
            {
                Path = NgJsHelper.GetNgJsFilePath(Path.Combine(this.ComponentsPath, componentName), basename, JsExt),
                JsName = basename,
                NgName = componentName.FirstCharToLower()
            };
        }

        public NgJsBaseData GetDirectiveData(string directiveName)
        {
            var basename = directiveName + DefaultDirectiveSuffix;

            return new NgJsBaseData
            {
                Path = NgJsHelper.GetNgJsFilePath(Path.Combine(this.DirectivesPath, directiveName), basename, JsExt),
                JsName = basename,
                NgName = directiveName.FirstCharToLower()
            };
        }

        public NgJsBaseData GetFilterData(string filterName)
        {
            var basename = filterName + DefaultFilterSuffix;

            return new NgJsBaseData
            {
                Path = NgJsHelper.GetNgJsFilePath(Path.Combine(this.FiltersPath, filterName), basename, JsExt),
                JsName = basename,
                NgName = filterName.FirstCharToLower()
            };
        }

        public Dictionary<string, NgJsViewDeps> GetAllDependencies()
        {
            Dictionary<string, NgJsViewDeps> deps = this.GetCurrentViewSpecificDependencies();

            NgJsViewDeps defaultDeps = this.GetDefaultDependencies();

            if (defaultDeps != null)
            {
                deps.Add("__DEFAULT__", defaultDeps);
            }

            return deps;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        private NgJsViewDeps GetDefaultDependencies()
        {
            return NgJsDefaultConventionHelper.GetDefaultDependencies();
        }

        private Dictionary<string, NgJsViewDeps> GetCurrentViewSpecificDependencies()
        {
            return NgJsViewDependenciesContext.Current.Dependencies;
        }
    }
}