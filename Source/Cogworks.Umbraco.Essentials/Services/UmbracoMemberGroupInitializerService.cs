using System;
using System.Collections.Generic;
using Cogworks.Umbraco.Essentials.Extensions;
using Cogworks.Umbraco.Essentials.Services.Interfaces;
using Umbraco.Core.Models;
using Umbraco.Core.Services;

namespace Cogworks.Umbraco.Essentials.Services
{
    public class UmbracoMemberGroupInitializerService : IUmbracoMemberGroupInitializerService
    {
        private readonly IMemberGroupService _memberGroupService;

        public UmbracoMemberGroupInitializerService(IMemberGroupService memberGroupService)
            => _memberGroupService = memberGroupService
                                     ?? throw new ArgumentNullException(nameof(memberGroupService));

        public void Initialize(string groupName)
        {
            if (!groupName.HasValue())
            {
                return;
            }

            var memberGroup = _memberGroupService.GetByName(groupName);

            if (memberGroup.HasValue())
            {
                return;
            }

            memberGroup = new MemberGroup()
            {
                Name = groupName
            };

            _memberGroupService.Save(memberGroup);
        }

        public void Initialize(IEnumerable<string> groupNames)
        {
            if (!groupNames.HasAny())
            {
                return;
            }

            foreach (var groupName in groupNames)
            {
                Initialize(groupName);
            }
        }
    }
}