
namespace Imbick.StarCitizen.Api.Tests.Models {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Imbick.StarCitizen.Api.Models;
    using Xunit;

    public class OrgsSearchRequestTests
    {
        [Fact]
        public void Ctor_WithBooleanYes_MapsCorrectly() {
            var request = new OrgsSearchRequest {
                Recruiting = OrgsSearchRequest.Boolean.Yes,
                RolePlay = OrgsSearchRequest.Boolean.Yes
            };
            var result = new OrgsSearchRequestInternal(request);
            Assert.True(result.Recruiting.Count() == 1);
            Assert.True(result.Recruiting.First() == 1);
            Assert.True(result.RolePlay.Count() == 1);
            Assert.True(result.RolePlay.First() == 1);
        }

        [Fact]
        public void Ctor_WithBooleanNo_MapsCorrectly() {
            var request = new OrgsSearchRequest {
                Recruiting = OrgsSearchRequest.Boolean.No,
                RolePlay = OrgsSearchRequest.Boolean.No
            };
            var result = new OrgsSearchRequestInternal(request);
            Assert.True(result.Recruiting.Count() == 1);
            Assert.True(result.Recruiting.First() == 0);
            Assert.True(result.RolePlay.Count() == 1);
            Assert.True(result.RolePlay.First() == 0);
        }

        [Fact]
        public void Ctor_WithBooleanBoth_MapsCorrectly() {
            var request = new OrgsSearchRequest {
                Recruiting = OrgsSearchRequest.Boolean.YesAndNo,
                RolePlay = OrgsSearchRequest.Boolean.YesAndNo
            };
            var result = new OrgsSearchRequestInternal(request);
            AssertContains(result.Recruiting, new List<int>());
            AssertContains(result.RolePlay, new List<int>());
        }

        [Fact]
        public void Ctor_WithSort_MapsCorrectly() {
            var result = new OrgsSearchRequestInternal(new OrgsSearchRequest { Sort = OrgsSearchRequest.OrganisationSorting.ActiveAscending});
            Assert.Equal(result.Sort, OrgsSearchRequestInternal.ActiveAsc);
            result = new OrgsSearchRequestInternal(new OrgsSearchRequest { Sort = OrgsSearchRequest.OrganisationSorting.ActiveDescending });
            Assert.Equal(result.Sort, OrgsSearchRequestInternal.ActiveDesc);
            result = new OrgsSearchRequestInternal(new OrgsSearchRequest { Sort = OrgsSearchRequest.OrganisationSorting.CreatedAscending});
            Assert.Equal(result.Sort, OrgsSearchRequestInternal.CreatedAsc);
            result = new OrgsSearchRequestInternal(new OrgsSearchRequest { Sort = OrgsSearchRequest.OrganisationSorting.CreatedDescending});
            Assert.Equal(result.Sort, OrgsSearchRequestInternal.CreatedDesc);
            result = new OrgsSearchRequestInternal(new OrgsSearchRequest { Sort = OrgsSearchRequest.OrganisationSorting.NameAscending});
            Assert.Equal(result.Sort, OrgsSearchRequestInternal.NameAsc);
            result = new OrgsSearchRequestInternal(new OrgsSearchRequest { Sort = OrgsSearchRequest.OrganisationSorting.NameDescending});
            Assert.Equal(result.Sort, OrgsSearchRequestInternal.NameDesc);
            result = new OrgsSearchRequestInternal(new OrgsSearchRequest { Sort = OrgsSearchRequest.OrganisationSorting.SizeAscending});
            Assert.Equal(result.Sort, OrgsSearchRequestInternal.SizeAsc);
            result = new OrgsSearchRequestInternal(new OrgsSearchRequest { Sort = OrgsSearchRequest.OrganisationSorting.SizeDescending });
            Assert.Equal(result.Sort, OrgsSearchRequestInternal.SizeDesc);
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                result = new OrgsSearchRequestInternal(new OrgsSearchRequest {Sort = (OrgsSearchRequest.OrganisationSorting)123}));
        }

        [Fact]
        public void Ctor_WithSize_MapsCorrectly() {
            var result = new OrgsSearchRequestInternal(new OrgsSearchRequest { Size = new[] { OrgSize.Large } });
            AssertContains(result.Size, new[] { OrgSize.Large.ToString().ToLower() });
            result = new OrgsSearchRequestInternal(new OrgsSearchRequest { Size = new[] { OrgSize.Medium } });
            AssertContains(result.Size, new[] { OrgSize.Medium.ToString().ToLower() });
            result = new OrgsSearchRequestInternal(new OrgsSearchRequest { Size = new[] { OrgSize.Small } });
            AssertContains(result.Size, new[] { OrgSize.Small.ToString().ToLower() });
            result = new OrgsSearchRequestInternal(new OrgsSearchRequest { Size = new[] { OrgSize.Small, OrgSize.Medium, OrgSize.Large } });
            AssertContains(result.Size, new[] { OrgSize.Small.ToString().ToLower(), OrgSize.Medium.ToString().ToLower(), OrgSize.Large.ToString().ToLower() });
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                result = new OrgsSearchRequestInternal(new OrgsSearchRequest { Size = new [] { (OrgSize)123} }));
        }

        [Fact]
        public void Ctor_WithCommitment_MapsCorrectly() {
            var result = new OrgsSearchRequestInternal(new OrgsSearchRequest { Commitment = new[] { OrgCommitment.Regular } });
            AssertContains(result.Commitment, new[] { "RE" });
            result = new OrgsSearchRequestInternal(new OrgsSearchRequest { Commitment = new[] { OrgCommitment.Casual } });
            AssertContains(result.Commitment, new[] { "CA" });
            result = new OrgsSearchRequestInternal(new OrgsSearchRequest { Commitment = new[] { OrgCommitment.Hardcore } });
            AssertContains(result.Commitment, new[] { "HA" });
            result = new OrgsSearchRequestInternal(new OrgsSearchRequest { Commitment = new[] { OrgCommitment.Casual, OrgCommitment.Hardcore } });
            AssertContains(result.Commitment, new[] { "CA", "HA" });
            /*Assert.Throws<ArgumentOutOfRangeException>(() =>
                result = new SearchRequestInternal(new SearchRequest { Commitment = new[] { (Commitment)123 } }));*/
        }

        private static void AssertContains<T>(IEnumerable<T> collection, IReadOnlyCollection<T> expectedContent) {
            Assert.True(collection.Count() == expectedContent.Count);
            foreach (var i in expectedContent) {
                Assert.Contains(i, collection);
            }
        }
    }
}
