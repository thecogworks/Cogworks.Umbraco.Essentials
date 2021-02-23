using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cogworks.Umbraco.Essentials.Extensions;
using Umbraco.Core;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using Umbraco.Web.Routing;

namespace Cogworks.Umbraco.Essentials.Helpers
{
    public static class DomainHelpers
    {
        public static IPublishedContent GetRootNodeFromDomain(HttpContextBase context, IUmbracoContextFactory umbracoContextFactory = null)
        {
            if (!umbracoContextFactory.HasValue())
            {
                umbracoContextFactory = DependencyResolver.Current.GetService<IUmbracoContextFactory>();
            }

            var requestUri = context.Request.Url;
            var umbracoContext = umbracoContextFactory.EnsureUmbracoContext().UmbracoContext;
            var domain = GetDomainByUri(umbracoContext, requestUri);

            if (domain == null)
            {
                return null;
            }

            var content = GetContentByDomain(umbracoContext, domain);
            return content;
        }

        public static Domain GetDomainByUri(UmbracoContext umbracoContext, Uri uri)
        {
            var baseUrl = $"{uri.Host}/{uri.Segments[1].RemoveTrailingSlash()}";
            var domains = umbracoContext.Domains.GetAll(false).ToList();
            var domain = domains.FirstOrDefault(x => x.Name.Equals(baseUrl));

            if (domain.HasValue())
            {
                return domain;
            }

            var domainsAndUris = domains.Select(x => new DomainAndUri(x, uri));
            var baseDomain = GetBaseDomain(domainsAndUris, uri);

            return baseDomain;
        }

        public static IPublishedContent GetContentByDomain(UmbracoContext umbracoContext, Domain domain)
        {
            if (!domain.HasValue() || domain.ContentId < 1)
            {
                return null;
            }

            var content = umbracoContext.Content.GetById(domain.ContentId);
            return content;
        }

        private static DomainAndUri GetBaseDomain(IEnumerable<DomainAndUri> domainsAndUris, Uri uri)
            => domainsAndUris.FirstOrDefault(x => x.Uri.EndPathWithSlash().IsBaseOf(uri));
    }
}