using System.Collections.Generic;

namespace Cogworks.Umbraco.Essentials.Services.Interfaces
{
    public interface IUmbracoMemberGroupInitializerService
    {
        void Initialize(string groupName);

        void Initialize(IEnumerable<string> groupNames);
    }
}