// <copyright file="DocumentTypeHelper.cs" company="Sintra">
// Copyright (c) Sintra. All rights reserved.
// </copyright>

namespace UmbracoAngularJs.Helpers
{
    using System;
    using Umbraco.Core.Models;

    /// <summary>
    /// Helper class to deal with document types.
    /// </summary>
    public static class DocumentTypeHelper
    {
        /// <summary>
        /// Gets the document type of the AngularJS master page.
        /// </summary>
        /// <returns>The document type definition.</returns>
        public static IContentType GetAngularJsMasterDocType()
        {
            // Main info
            ContentType res = new ContentType(-1)
            {
                Name = "Sintra - AngularJs Master",
                Alias = "sintraAngularJsMaster",
                AllowedAsRoot = true,
                Description = "This should be the master template for all views.",
                CreateDate = DateTime.Now
            };

            res.UpdateDate = res.CreateDate;

            // Angular JS related properties
            res.AddPropertyGroup("AngularJs");

            var enableJsPropType = new PropertyType(new DataTypeDefinition("Umbraco.TrueFalse"), "enableNg");
            enableJsPropType.Name = "Enable AngularJS?";
            res.AddPropertyType(enableJsPropType, "AngularJs");

            return res;
        }
    }
}