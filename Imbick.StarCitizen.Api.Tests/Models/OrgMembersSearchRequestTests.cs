
namespace Imbick.StarCitizen.Api.Tests.Models {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Imbick.StarCitizen.Api.Models;
    using Xunit;

    public class OrgMembersSearchRequestTests
    {
        [Fact]
        public void Ctor_WithMembershipMainOrg_MapsCorrectly()
        {
            var request = new OrgMembersSearchRequest
            {
                Membership = OrgMembersSearchRequest.MembershipType.MainOrg
            };
            var result = new OrgMembersSearchRequestInternal(request);
            Assert.True(result.Main_Org == 1);
        }

        [Fact]
        public void Ctor_WithMembershipAffiliate_MapsCorrectly()
        {
            var request = new OrgMembersSearchRequest
            {
                Membership = OrgMembersSearchRequest.MembershipType.Affiliate
            };
            var result = new OrgMembersSearchRequestInternal(request);
            Assert.True(result.Main_Org == 0);
        }

        [Fact]
        public void Ctor_WithMembershipAny_MapsCorrectly()
        {
            var request = new OrgMembersSearchRequest
            {
                Membership = OrgMembersSearchRequest.MembershipType.Any
            };
            var result = new OrgMembersSearchRequestInternal(request);
            Assert.True(result.Main_Org == null);
        }

    }
}
