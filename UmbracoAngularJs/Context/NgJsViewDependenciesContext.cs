// <copyright file="NgJsViewDependenciesContext.cs" company="Sintra">
// Copyright (c) Sintra. All rights reserved.
// </copyright>

namespace UmbracoAngularJs.Context
{
    using System.Collections.Generic;
    using System.Web;
    using UmbracoAngularJs.Classes;

    /// <summary>
    /// AngularJS dependencies context.
    /// </summary>
    public class NgJsViewDependenciesContext
    {
        private static readonly string HttpContextKey = "ngJsViewDepsContext";

        /// <summary>
        /// Initializes a new instance of the <see cref="NgJsViewDependenciesContext"/> class.
        /// </summary>
        public NgJsViewDependenciesContext()
        {
            Dependencies = new Dictionary<string, NgJsViewDeps>();
        }

        /// <summary>
        /// Gets the current instance of <see cref="NgJsViewDependenciesContext"/>.
        /// </summary>
        /// <value>
        /// The current instance of <see cref="NgJsViewDependenciesContext"/>.
        /// </value>
        public static NgJsViewDependenciesContext Current => (HttpContext.Current.Items[HttpContextKey] as NgJsViewDependenciesContext)
                    ?? (HttpContext.Current.Items[HttpContextKey]
                    = new NgJsViewDependenciesContext()) as NgJsViewDependenciesContext;

        /// <summary>
        /// Gets or sets the AngularJS initialize script.
        /// </summary>
        /// <value>
        /// The AngularJS initialize script.
        /// </value>
        public string InitScript { get; set; }

        /// <summary>
        /// Gets or sets the AngularJS dependencies, grouped by view name.
        /// </summary>
        /// <value>
        /// The AngularJS dependencies.
        /// </value>
        public Dictionary<string, NgJsViewDeps> Dependencies { get; set; }

        public Dictionary<string, string> I18n { get; set; }
    }
}