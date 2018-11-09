// <copyright file="UmbracoAngularJsEventHandler.cs" company="Sintra">
// Copyright (c) Sintra. All rights reserved.
// </copyright>

namespace UmbracoAngularJs
{
    using System.Linq;
    using Umbraco.Core;
    using UmbracoAngularJs.Helpers;
    using UmbracoUtils.Helpers;

    public class UmbracoAngularJsEventHandler : ApplicationEventHandler
    {
        private void EnsureAngularJsMasterExists()
        {
            // crea un'istanza del documento Ng di base
            var ngJsMasterDocType = DocumentTypeHelper.GetAngularJsMasterDocType();
            var currentNgDocType = ApplicationContext.Current.Services.ContentTypeService.GetContentType(ngJsMasterDocType.Alias);

            if (currentNgDocType == null)
            {
                ApplicationContext.Current.Services.ContentTypeService.Save(ngJsMasterDocType);
            }
            else
            {
                // proprietà aggiornabili senza effetti collaterali
                currentNgDocType.CreateDate = ngJsMasterDocType.CreateDate;
                currentNgDocType.Icon = ngJsMasterDocType.Icon;
                currentNgDocType.Name = ngJsMasterDocType.Name;
                currentNgDocType.Thumbnail = ngJsMasterDocType.Thumbnail;
                currentNgDocType.UpdateDate = ngJsMasterDocType.UpdateDate;

                // TODO update
                // identificare le proprietà da aggiornare
                // e quelle da impostare obbligatoriamente,
                // altrimenti copiando a tappeto tutte le proprietà
                // si rischia di riportare valori nulli o non validi
                // con effetti collaterali in esecuzione
                // Es. check enableAngularJs nel backend
                //currentNgDocType.Alias = ngJsMasterDocType.Alias;
                //currentNgDocType.AllowedAsRoot = ngJsMasterDocType.AllowedAsRoot;
                //currentNgDocType.AllowedContentTypes = ngJsMasterDocType.AllowedContentTypes;
                //currentNgDocType.AllowedTemplates = ngJsMasterDocType.AllowedTemplates;
                //currentNgDocType.ContentTypeComposition = ngJsMasterDocType.ContentTypeComposition;
                //currentNgDocType.SetDefaultTemplate(ngJsMasterDocType.DefaultTemplate);
                //currentNgDocType.CreatorId = ngJsMasterDocType.CreatorId;
                //currentNgDocType.Description = ngJsMasterDocType.Description;
                //currentNgDocType.IsContainer = ngJsMasterDocType.IsContainer;
                //currentNgDocType.Level = ngJsMasterDocType.Level;
                //currentNgDocType.NoGroupPropertyTypes = ngJsMasterDocType.NoGroupPropertyTypes;
                //currentNgDocType.ParentId = ngJsMasterDocType.ParentId;
                //currentNgDocType.Path = ngJsMasterDocType.Path;
                //currentNgDocType.SortOrder = ngJsMasterDocType.SortOrder;

                foreach (var defaultPropertyGroup in ngJsMasterDocType.PropertyGroups)
                {
                    // se il docType attuale non contiene i "tab" predefiniti, allora vengono aggiunti
                    if (!currentNgDocType.PropertyGroups.Contains(defaultPropertyGroup.Name))
                    {
                        currentNgDocType.PropertyGroups.Add(defaultPropertyGroup);
                    }
                }

                foreach (var defaultPropertyType in ngJsMasterDocType.PropertyTypes)
                {
                    // se il docType attuale non contiene le proprietà predefinite, allora vengono aggiunte
                    if (!currentNgDocType.PropertyTypes.Contains(defaultPropertyType))
                    {
                        currentNgDocType.AddPropertyType(defaultPropertyType);
                    }
                }
            }

            ApplicationContext.Current.Services.ContentTypeService.Save(currentNgDocType);
        }


        protected override void ApplicationStarted(
            UmbracoApplicationBase umbracoApplication,
            ApplicationContext applicationContext)
        {
            ////var contentTypeService = ApplicationContext.Current.Services.ContentTypeService;

            //// crea un'istanza del documento Ng di base
            //var ngJsMasterDocType = DocumentTypeHelper.GetAngularJsMasterDocType();
            //// aggiunge o aggiorna il documento Ng
            //UmbracoContentTypeHelper.AddOrUpdateDocType(ngJsMasterDocType);

            this.EnsureAngularJsMasterExists();
        }
    }
}