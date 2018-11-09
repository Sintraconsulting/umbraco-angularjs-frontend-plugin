// <copyright file="IContentTypeExtension.cs" company="Sintra">
// Copyright (c) Sintra. All rights reserved.
// </copyright>

namespace UmbracoUtils.Extensions
{
    using System.Linq;
    using Umbraco.Core.Models;

    public static class IContentTypeExtension
    {
        /// <summary>
        /// Override this object data with data from the other one. Modifies this object in place.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="other">The other.</param>
        /// <returns>This object after override.</returns>
        public static IContentType OverrideWith(this IContentType obj, IContentType other)
        {
            obj.Alias = other.Alias;
            obj.AllowedAsRoot = other.AllowedAsRoot;
            obj.AllowedContentTypes = other.AllowedContentTypes;
            obj.AllowedTemplates = other.AllowedTemplates;
            obj.ContentTypeComposition = other.ContentTypeComposition;
            obj.SetDefaultTemplate(other.DefaultTemplate);

            obj.CreateDate = other.CreateDate;
            obj.CreatorId = other.CreatorId;
            //obj.DeletedDate = other.DeletedDate;

            obj.Description = other.Description;
            obj.Icon = other.Icon;
            obj.IsContainer = other.IsContainer;
            obj.Level = other.Level;
            obj.Name = other.Name;
            obj.NoGroupPropertyTypes = other.NoGroupPropertyTypes;

            obj.ParentId = other.ParentId;
            obj.Path = other.Path;

            // fix: rimuoveva i gruppi creati da backend
            // obj.PropertyGroups = other.PropertyGroups;
            foreach (PropertyGroup otherPropertyGroup in other.PropertyGroups)
            {
                if (obj.PropertyGroups.Contains(otherPropertyGroup.Name))
                {
                    obj.PropertyGroups.RemoveItem(otherPropertyGroup.Name);
                    obj.PropertyGroups.Add(otherPropertyGroup);
                }
            }

            foreach (PropertyType p in other.PropertyTypes)
            {
                if (!obj.PropertyTypes.Contains(p))
                {
                    obj.AddPropertyType(p);
                }
            }

            obj.SortOrder = other.SortOrder;
            obj.Thumbnail = other.Thumbnail;
            obj.UpdateDate = other.UpdateDate;

            return obj;
        }
    }
}