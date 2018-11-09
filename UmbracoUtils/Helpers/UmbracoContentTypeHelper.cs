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
            Umbraco.Core.Services.IContentTypeService contentTypeService = ApplicationContext.Current.Services.ContentTypeService;
            IContentType actualDocType = docType;

            IContentType oldContentType = contentTypeService.GetContentType(docType.Alias);
            actualDocType = oldContentType != null ? oldContentType.OverrideWith(docType) : docType;

            actualDocType.UpdateDate = DateTime.Now;

            contentTypeService.Save(actualDocType);
        }
    }
}