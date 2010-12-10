﻿using System.Globalization;
using JetBrains.Annotations;
using Orchard.Core.Localization.Models;
using Orchard.Data;
using Orchard.Localization;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Handlers;
using Orchard.Localization.Services;

namespace Orchard.Core.Localization.Handlers {
    [UsedImplicitly]
    public class LocalizationPartHandler : ContentHandler {
        private readonly ICultureManager _cultureManager;
        private readonly IContentManager _contentManager;

        public LocalizationPartHandler(IRepository<LocalizationPartRecord> localizedRepository, ICultureManager cultureManager, IContentManager contentManager) {
            _cultureManager = cultureManager;
            _contentManager = contentManager;
            T = NullLocalizer.Instance;

            Filters.Add(StorageFilter.For(localizedRepository));

            OnInitializing<LocalizationPart>(InitializePart);

            OnIndexed<LocalizationPart>((context, localized) => context.DocumentIndex
                .Add("culture", CultureInfo.GetCultureInfo(localized.Culture != null ? localized.Culture.Culture : _cultureManager.GetSiteCulture()).LCID)
                .Store()
                );
        }

        public Localizer T { get; set; }

        void InitializePart(InitializingContentContext context, LocalizationPart localizationPart) {
            localizationPart.CultureField.Setter(cultureRecord => {
                localizationPart.Record.CultureId = cultureRecord.Id;
                return cultureRecord;
            });
            localizationPart.MasterContentItemField.Setter(masterContentItem => {
                localizationPart.Record.MasterContentItemId = masterContentItem.ContentItem.Id;
                return masterContentItem;
            });
            localizationPart.CultureField.Loader(ctx => _cultureManager.GetCultureById(localizationPart.Record.CultureId));
            localizationPart.MasterContentItemField.Loader(ctx => _contentManager.Get(localizationPart.Record.MasterContentItemId)); 
        }
    }
}