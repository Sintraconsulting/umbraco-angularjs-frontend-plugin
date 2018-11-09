// <copyright file="NgJsDefaultConventionHelper.cs" company="Sintra">
// Copyright (c) Sintra. All rights reserved.
// </copyright>

namespace UmbracoAngularJs.Helpers
{
    using System;
    using System.Configuration;
    using System.Linq;
    using UmbracoAngularJs.Classes;

    /// <summary>
    /// Internal helper to retrieve AngularJs config with default convention.
    /// </summary>
    internal class NgJsDefaultConventionHelper
    {
        private static readonly string AppSettingsPrefix = "ngjs.";

        private static readonly string ViewsDirConfigKey = "viewsDir";
        private static readonly string DefaultViewsDir = "~/Ng/Views/";

        private static readonly string ServicesDirConfigKey = "servicesDir";
        private static readonly string DefaultServicesDir = "~/Ng/Services/";

        private static readonly string ConfigsDirConfigKey = "configsDir";
        private static readonly string DefaultConfigsDir = "~/Ng/Configs/";

        private static readonly string ComponentsDirConfigKey = "componentsDir";
        private static readonly string DefaultComponentsDir = "~/Ng/Components/";

        private static readonly string DirectivesDirConfigKey = "directivesDir";
        private static readonly string DefaultDirectivesDir = "~/Ng/Directives/";

        private static readonly string FiltersDirConfigKey = "filtersDir";
        private static readonly string DefaultFiltersDir = "~/Ng/Filters/";


        private static readonly string DefaultDepsDirConfigKey = "defaultDeps";

        /// <summary>
        /// Gets the AngularJS views path on server.
        /// </summary>
        /// <returns>The AngularJS views path on server./returns>
        public static string GetViewsPath()
        {
            string viewsDir;
            return TryGetConfigFromAppSettings(ViewsDirConfigKey, out viewsDir) ? viewsDir : DefaultViewsDir;
        }

        /// <summary>
        /// Gets the AngularJS services path on server.
        /// </summary>
        /// <returns>The AngularJS services path on server./returns>
        public static string GetServicesPath()
        {
            string servicesDir;
            return
                TryGetConfigFromAppSettings(ServicesDirConfigKey, out servicesDir) ? servicesDir : DefaultServicesDir;
        }

        /// <summary>
        /// Gets the AngularJS configs path on server.
        /// </summary>
        /// <returns>The AngularJS configs path on server./returns>
        public static string GetConfigsPath()
        {
            string configDir;
            return TryGetConfigFromAppSettings(ConfigsDirConfigKey, out configDir) ? configDir : DefaultConfigsDir;
        }

        /// <summary>
        /// Gets the AngularJS components path on server.
        /// </summary>
        /// <returns>The AngularJS components path on server./returns>
        public static string GetComponentsPath()
        {
            string componentsDir;
            return TryGetConfigFromAppSettings(ComponentsDirConfigKey, out componentsDir)
                ? componentsDir : DefaultComponentsDir;
        }

        /// <summary>
        /// Gets the AngularJS directives path on server.
        /// </summary>
        /// <returns>The AngularJS directives path on server./returns>
        public static string GetDirectivesPath()
        {
            string directivesDir;
            return TryGetConfigFromAppSettings(DirectivesDirConfigKey, out directivesDir)
                ? directivesDir : DefaultDirectivesDir;
        }

        /// <summary>
        /// Gets the AngularJS filters path on server.
        /// </summary>
        /// <returns>The AngularJS filters path on server./returns>
        public static string GetFiltersPath()
        {
            string filtersDir;
            return TryGetConfigFromAppSettings(FiltersDirConfigKey, out filtersDir)
                ? filtersDir : DefaultFiltersDir;
        }

        /// <summary>
        /// Gets the default AngularJS dependencies (modules), to be inherited by all defined components.
        /// </summary>
        /// <returns>The default AngularJS deps.</returns>
        public static NgJsViewDeps GetDefaultDependencies()
        {
            NgJsViewDeps res = new NgJsViewDeps();

            string defaultDeps;
            if (!TryGetConfigFromAppSettings(DefaultDepsDirConfigKey, out defaultDeps))
            {
                return res;
            }

            res.Modules = new System.Collections.Generic.List<string>(defaultDeps.Split(','));
            return res;
        }

        private static bool TryGetConfigFromAppSettings(string ngJsConfigKey, out string value)
        {
            var completeConfigKey = AppSettingsPrefix + ngJsConfigKey;
            if (string.IsNullOrEmpty(completeConfigKey))
            {
                throw new ArgumentException("'configKey' cannot be null or empty");
            }

            if (ConfigurationManager.AppSettings.AllKeys.Contains(completeConfigKey))
            {
                value = null;
                return false;
            }

            value = ConfigurationManager.AppSettings[completeConfigKey];
            return !string.IsNullOrEmpty(value);
        }
    }
}