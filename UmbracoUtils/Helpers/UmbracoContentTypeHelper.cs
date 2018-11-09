// <copyright file="UmbracoContentTypeHelper.cs" company="Sintra">
// Copyright (c) Sintra. All rights reserved.
// </copyright>

namespace UmbracoUtils.Helpers
{
    using System;
    using Umbraco.Core;
    using Umbraco.Core.Models;
    using UmbracoUtils.Extensions;

    public static class UmbracoContentTypeHelper
    {
        public static void AddOrUpdateDocType(IContentType docType)
        {
            var contentTypeService = ApplicationContext.Current.Services.ContentTypeService;
            var actualDocType = docType;

            var oldContentType = contentTypeService.GetContentType(docType.Alias);
            actualDocType = oldContentType != null ? oldContentType.OverrideWith(docType) : docType;

            actualDocType.UpdateDate = DateTime.Now;

            contentTypeService.Save(actualDocType);
        }
    }
}