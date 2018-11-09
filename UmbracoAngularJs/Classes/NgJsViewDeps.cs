// <copyright file="NgJsViewDeps.cs" company="Sintra">
// Copyright (c) Sintra. All rights reserved.
// </copyright>

namespace UmbracoAngularJs.Classes
{
    using System.Collections.Generic;

    /// <summary>
    /// Class holding info about AngularJS dependencies for a page.
    /// </summary>
    public class NgJsViewDeps
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NgJsViewDeps"/> class.
        /// </summary>
        public NgJsViewDeps()
        {
            this.Services = new List<string>();
            this.Modules = new List<string>();
            this.Components = new List<string>();
            this.Directives = new List<string>();
            this.Filters = new List<string>();
        }

        /// <summary>
        /// Gets or sets the AngularJS services.
        /// </summary>
        /// <value>
        /// The services.
        /// </value>
        public List<string> Services { get; set; }

        /// <summary>
        /// Gets or sets the AngularJS modules.
        /// </summary>
        /// <value>
        /// The modules.
        /// </value>
        public List<string> Modules { get; set; }

        /// <summary>
        /// Gets or sets the AngularJS directives.
        /// </summary>
        /// <value>
        /// The directives.
        /// </value>
        public List<string> Directives { get; set; }

        /// <summary>
        /// Gets or sets the AngularJS components.
        /// </summary>
        /// <value>
        /// The components.
        /// </value>
        public List<string> Components { get; set; }

        /// <summary>
        /// Gets or sets the AngularJS filters.
        /// </summary>
        /// <value>
        /// The filters.
        /// </value>
        public List<string> Filters { get; set; }

        /// <summary>
        /// Gets or sets the AngularJS main view (represented by a controller).
        /// </summary>
        /// <value>
        /// The main view.
        /// </value>
        internal string View { get; set; } = null;
    }
}