// <copyright file="NgJsBaseData.cs" company="Sintra">
// Copyright (c) Sintra. All rights reserved.
// </copyright>

namespace UmbracoAngularJs.Classes
{
    /// <summary>
    /// Holds data of an AngularJS element.
    /// </summary>
    public class NgJsBaseData
    {
        private string ngName = null;

        /// <summary>
        /// Gets or sets the file path.
        /// </summary>
        /// <value>
        /// The file path.
        /// </value>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the name to be used in JS scripts.
        /// </summary>
        /// <value>
        /// The JS name.
        /// </value>
        public string JsName { get; set; }

        /// <summary>
        /// Gets or sets the name to be used during component/directive/service registration in Angular world.
        /// </summary>
        /// <value>
        /// The AngularJS name.
        /// </value>
        public string NgName
        {
            get => ngName != null ? ngName : JsName;
            set => ngName = value;
        }
    }
}