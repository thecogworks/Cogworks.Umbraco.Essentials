using System;
using System.Collections.Generic;
using System.Linq;
using Cogworks.Umbraco.Essentials.Services.Interfaces;
using Umbraco.Core.Scoping;

namespace Cogworks.Umbraco.Essentials.Services
{
    public class PublicAccessService : IPublicAccessService
    {
        private readonly IScopeProvider _scopeProvider;

        public PublicAccessService(IScopeProvider scopeProvider)
            => _scopeProvider = scopeProvider
                                ?? throw new ArgumentNullException(nameof(scopeProvider));

        public IEnumerable<int> GetContentIdsWithRestrictedAccess()
        {
            using (var scope = _scopeProvider.CreateScope(autoComplete: true))
            {
                return scope.Database
                    .Query<int>("SELECT nodeId FROM umbracoAccess")
                    .ToList();
            }
        }
    }
}