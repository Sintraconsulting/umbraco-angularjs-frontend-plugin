// <copyright file="INgJsProvider.cs" company="Sintra">
// Copyright (c) Sintra. All rights reserved.
// </copyright>

namespace UmbracoAngularJs.Providers
{
    using System;
    using System.Collections.Generic;
    using UmbracoAngularJs.Classes;

    /// <summary>
    /// Interface for AngularJS injection provider.
    /// </summary>
    /// <seealso cref="System.ICloneable" />
    public interface INgJsProvider : ICloneable
    {
        /// <summary>
        /// Gets the name of the AngularJS application.
        /// </summary>
        /// <returns>The AngularJS app name.</returns>
        string GetAppName();

        /// <summary>
        /// Gets the data of a specific AngularJS module configuration.
        /// </summary>
        /// <param name="moduleName">Name of the module.</param>
        /// <returns>The data of the specified config.</returns>
        NgJsBaseData GetConfigData(string moduleName);

        /// <summary>
        /// Gets the data of a specific AngularJS view (i.e. a component with top logic).
        /// </summary>
        /// <param name="viewName">Name of the view.</param>
        /// <returns>The data of the specified view.</returns>
        NgJsViewData GetViewData(string viewName);

        /// <summary>
        /// Gets thedata of a specific AngularJS service.
        /// </summary>
        /// <param name="serviceName">Name of the service.</param>
        /// <returns>The data of the specified service.</returns>
        NgJsBaseData GetServiceData(string serviceName);

        /// <summary>
        /// Gets the data of a specific AngularJS component.
        /// </summary>
        /// <param name="componentName">Name of the component.</param>
        /// <returns>The data of the specified component.</returns>
        NgJsBaseData GetComponentData(string componentName);

        /// <summary>
        /// Gets the data of a specific AngularJS directive.
        /// </summary>
        /// <param name="directiveName">Name of the directive.</param>
        /// <returns>The data of the specified directive.</returns>
        NgJsBaseData GetDirectiveData(string directiveName);

        /// <summary>
        /// Gets the data of a specific AngularJS filter.
        /// </summary>
        /// <param name="filterName">Name of the filter.</param>
        /// <returns>The data of the specified filter.</returns>
        NgJsBaseData GetFilterData(string filterName);

        /// <summary>
        /// Gets ALL currently registered AngularJS dependencies, grouped by view.
        /// </summary>
        /// <returns>The registered AngularJS dependencies.</returns>
        Dictionary<string, NgJsViewDeps> GetAllDependencies();
    }
}