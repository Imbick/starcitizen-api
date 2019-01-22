
namespace Imbick.StarCitizen.Api.Tests.Models {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Imbick.StarCitizen.Api.Models;
    using Xunit;

    public class SearchRequestTests
    {
        [Fact]
        public void Ctor_WithBooleanYes_MapsCorrectly() {
            var request = new SearchRequest {
                Recruiting = SearchRequest.Boolean.Yes,
                RolePlay = SearchRequest.Boolean.Yes
            };
            var result = new SearchRequestInternal(request);
            Assert.True(result.Recruiting.Count() == 1);
            Assert.True(result.Recruiting.First() == 1);
            Assert.True(result.RolePlay.Count() == 1);
            Assert.True(result.RolePlay.First() == 1);
        }

        [Fact]
        public void Ctor_WithBooleanNo_MapsCorrectly() {
            var request = new SearchRequest {
                Recruiting = SearchRequest.Boolean.No,
                RolePlay = SearchRequest.Boolean.No
            };
            var result = new SearchRequestInternal(request);
            Assert.True(result.Recruiting.Count() == 1);
            Assert.True(result.Recruiting.First() == 0);
            Assert.True(result.RolePlay.Count() == 1);
            Assert.True(result.RolePlay.First() == 0);
        }

        [Fact]
        public void Ctor_WithBooleanBoth_MapsCorrectly() {
            var request = new SearchRequest {
                Recruiting = SearchRequest.Boolean.YesAndNo,
                RolePlay = SearchRequest.Boolean.YesAndNo
            };
            var result = new SearchRequestInternal(request);
            AssertContains(result.Recruiting, new List<int>());
            AssertContains(result.RolePlay, new List<int>());
        }

        [Fact]
        public void Ctor_WithSort_MapsCorrectly() {
            var result = new SearchRequestInternal(new SearchRequest { Sort = SearchRequest.Sorting.ActiveAscending});
            Assert.Equal(result.Sort, SearchRequestInternal.ActiveAsc);
            result = new SearchRequestInternal(new SearchRequest { Sort = SearchRequest.Sorting.ActiveDescending });
            Assert.Equal(result.Sort, SearchRequestInternal.ActiveDesc);
            result = new SearchRequestInternal(new SearchRequest { Sort = SearchRequest.Sorting.CreatedAscending});
            Assert.Equal(result.Sort, SearchRequestInternal.CreatedAsc);
            result = new SearchRequestInternal(new SearchRequest { Sort = SearchRequest.Sorting.CreatedDescending});
            Assert.Equal(result.Sort, SearchRequestInternal.CreatedDesc);
            result = new SearchRequestInternal(new SearchRequest { Sort = SearchRequest.Sorting.NameAscending});
            Assert.Equal(result.Sort, SearchRequestInternal.NameAsc);
            result = new SearchRequestInternal(new SearchRequest { Sort = SearchRequest.Sorting.NameDescending});
            Assert.Equal(result.Sort, SearchRequestInternal.NameDesc);
            result = new SearchRequestInternal(new SearchRequest { Sort = SearchRequest.Sorting.SizeAscending});
            Assert.Equal(result.Sort, SearchRequestInternal.SizeAsc);
            result = new SearchRequestInternal(new SearchRequest { Sort = SearchRequest.Sorting.SizeDescending });
            Assert.Equal(result.Sort, SearchRequestInternal.SizeDesc);
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                result = new SearchRequestInternal(new SearchRequest {Sort = (SearchRequest.Sorting)123}));
        }

        [Fact]
        public void Ctor_WithSize_MapsCorrectly() {
            var result = new SearchRequestInternal(new SearchRequest { Size = new[] { Size.Large } });
            AssertContains(result.Size, new[] { Size.Large.ToString().ToLower() });
            result = new SearchRequestInternal(new SearchRequest { Size = new[] { Size.Medium } });
            AssertContains(result.Size, new[] { Size.Medium.ToString().ToLower() });
            result = new SearchRequestInternal(new SearchRequest { Size = new[] { Size.Small } });
            AssertContains(result.Size, new[] { Size.Small.ToString().ToLower() });
            result = new SearchRequestInternal(new SearchRequest { Size = new[] { Size.Small, Size.Medium, Size.Large } });
            AssertContains(result.Size, new[] { Size.Small.ToString().ToLower(), Size.Medium.ToString().ToLower(), Size.Large.ToString().ToLower() });
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                result = new SearchRequestInternal(new SearchRequest { Size = new [] { (Size)123} }));
        }

        [Fact]
        public void Ctor_WithCommitment_MapsCorrectly() {
            var result = new SearchRequestInternal(new SearchRequest { Commitment = new[] { Commitment.Regular } });
            AssertContains(result.Commitment, new[] { "RE" });
            result = new SearchRequestInternal(new SearchRequest { Commitment = new[] { Commitment.Casual } });
            AssertContains(result.Commitment, new[] { "CA" });
            result = new SearchRequestInternal(new SearchRequest { Commitment = new[] { Commitment.Hardcore } });
            AssertContains(result.Commitment, new[] { "HA" });
            result = new SearchRequestInternal(new SearchRequest { Commitment = new[] { Commitment.Casual, Commitment.Hardcore } });
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
