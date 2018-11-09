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
            return TryGetConfigFromAppSettings(ViewsDirConfigKey, out string viewsDir) ? viewsDir : DefaultViewsDir;
        }

        /// <summary>
        /// Gets the AngularJS services path on server.
        /// </summary>
        /// <returns>The AngularJS services path on server./returns>
        public static string GetServicesPath()
        {
            return
                TryGetConfigFromAppSettings(ServicesDirConfigKey, out string servicesDir) ? servicesDir : DefaultServicesDir;
        }

        /// <summary>
        /// Gets the AngularJS configs path on server.
        /// </summary>
        /// <returns>The AngularJS configs path on server./returns>
        public static string GetConfigsPath()
        {
            return TryGetConfigFromAppSettings(ConfigsDirConfigKey, out string configDir) ? configDir : DefaultConfigsDir;
        }

        /// <summary>
        /// Gets the AngularJS components path on server.
        /// </summary>
        /// <returns>The AngularJS components path on server./returns>
        public static string GetComponentsPath()
        {
            return TryGetConfigFromAppSettings(ComponentsDirConfigKey, out string componentsDir)
                ? componentsDir : DefaultComponentsDir;
        }

        /// <summary>
        /// Gets the AngularJS directives path on server.
        /// </summary>
        /// <returns>The AngularJS directives path on server./returns>
        public static string GetDirectivesPath()
        {
            return TryGetConfigFromAppSettings(DirectivesDirConfigKey, out string directivesDir)
                ? directivesDir : DefaultDirectivesDir;
        }

        /// <summary>
        /// Gets the AngularJS filters path on server.
        /// </summary>
        /// <returns>The AngularJS filters path on server./returns>
        public static string GetFiltersPath()
        {
            return TryGetConfigFromAppSettings(FiltersDirConfigKey, out string filtersDir)
                ? filtersDir : DefaultFiltersDir;
        }

        /// <summary>
        /// Gets the default AngularJS dependencies (modules), to be inherited by all defined components.
        /// </summary>
        /// <returns>The default AngularJS deps.</returns>
        public static NgJsViewDeps GetDefaultDependencies()
        {
            NgJsViewDeps res = new NgJsViewDeps();

            if (!TryGetConfigFromAppSettings(DefaultDepsDirConfigKey, out string defaultDeps))
            {
                return res;
            }

            res.Modules = new System.Collections.Generic.List<string>(defaultDeps.Split(','));
            return res;
        }

        private static bool TryGetConfigFromAppSettings(string ngJsConfigKey, out string value)
        {
            string completeConfigKey = AppSettingsPrefix + ngJsConfigKey;
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