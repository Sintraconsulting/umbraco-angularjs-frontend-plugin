// <copyright file="NgJsViewData.cs" company="Sintra">
// Copyright (c) Sintra. All rights reserved.
// </copyright>

namespace UmbracoAngularJs.Classes
{
    /// <summary>
    /// Holder about paths of an MVC view with integrated AngularJS.
    /// </summary>
    public class NgJsViewData : NgJsBaseData
    {
        /// <summary>
        /// Gets or sets the template path.
        /// </summary>
        /// <value>
        /// The template path.
        /// </value>
        public string TemplatePath { get; set; } = null;

        /// <summary>
        /// Gets or sets the name of the initialize function in JS.
        /// </summary>
        /// <value>
        /// The name of the initialize function in JS.
        /// </value>
        public string InitFunctionJsName { get; set; } = null;
    }
}