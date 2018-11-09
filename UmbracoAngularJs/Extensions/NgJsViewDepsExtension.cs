// <copyright file="NgJsViewDepsExtension.cs" company="Sintra">
// Copyright (c) Sintra. All rights reserved.
// </copyright>

namespace UmbracoAngularJs.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using UmbracoAngularJs.Classes;

    /// <summary>
    /// Class holding info about AngularJS dependencies for a page.
    /// </summary>
    public static class NgJsViewDepsExtension
    {
        /// <summary>
        /// Merges the specified dependencies object into this one. This object will be modified in-place.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="other">The other object.</param>
        public static void Merge(this NgJsViewDeps obj, NgJsViewDeps other)
        {
            if (other == null)
            {
                return;
            }

            MergeNotPresent(obj.Modules, other.Modules);
            MergeNotPresent(obj.Services, other.Services);
            MergeNotPresent(obj.Directives, other.Directives);
            MergeNotPresent(obj.Components, other.Components);
            MergeNotPresent(obj.Filters, other.Filters);
        }

        private static void MergeNotPresent(List<string> target, List<string> other)
        {
            if (other != null)
            {
                foreach (var o in other)
                {
                    if (!target.Contains(o))
                    {
                        target.Add(o);
                    }
                }
            }
        }
    }
}