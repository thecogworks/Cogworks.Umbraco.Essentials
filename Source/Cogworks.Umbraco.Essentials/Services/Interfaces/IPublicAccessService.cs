using System.Collections.Generic;

namespace Cogworks.Umbraco.Essentials.Services.Interfaces
{
    public interface IPublicAccessService
    {
        IEnumerable<int> GetContentIdsWithRestrictedAccess();
    }
}