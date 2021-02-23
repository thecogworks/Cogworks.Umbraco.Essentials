using System.Collections.Generic;
using Umbraco.Core;
using Umbraco.Core.Composing;
using Umbraco.Core.Services;
using Umbraco.Examine;

namespace Cogworks.Umbraco.Essentials.Composers
{
    public class ExternalIndexComposer : IUserComposer
    {
        public void Compose(Composition composition)
        {
            // replace the default IUmbracoIndexConfig definition
            composition.RegisterUnique<IUmbracoIndexConfig, ExternalIndexConfig>();
        }
    }

    public class ExternalIndexConfig : UmbracoIndexConfig, IUmbracoIndexConfig
    {
        private readonly IPublicAccessService _publicAccessService;

        public ExternalIndexConfig(IPublicAccessService publicAccessService) : base(publicAccessService)
            => _publicAccessService = publicAccessService;

        IContentValueSetValidator IUmbracoIndexConfig.GetPublishedContentValueSetValidator()
            => new ContentValueSetValidator(true,
                true,
                _publicAccessService,
                includeItemTypes: new List<string>()
            );
    }
}