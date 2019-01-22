
namespace Imbick.StarCitizen.Api.Models {
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class SearchRequest {
        public enum Sorting {
            ActiveAscending,
            ActiveDescending,
            SizeAscending,
            SizeDescending,
            NameAscending,
            NameDescending,
            CreatedAscending,
            CreatedDescending
        }

        public enum Boolean {
            YesAndNo,
            Yes,
            No
        }

        public Sorting Sort { get; set; }
        public string Search { get; set; }
        public string Language { get; set; } //todo enumerate these
        public ICollection<Commitment> Commitment { get; set; } = new List<Commitment>();
        public ICollection<Size> Size { get; set; } = new List<Size>();
        public ICollection<Archetype> Model => new List<Archetype>();
        public ICollection<Activity> Activity => new List<Activity>();
        public Boolean RolePlay { get; set; }
        public Boolean Recruiting { get; set; }
        public int PageSize { get; set; } = 12;
        public int Page { get; set; } = 1;
    }

    public class SearchRequestInternal {
        public static string SizeAsc = "size_asc";
        public static string SizeDesc = "size_desc";
        public static string NameAsc = "name_asc";
        public static string NameDesc = "name_desc";
        public static string CreatedAsc = "created_asc";
        public static string CreatedDesc = "created_desc";
        public static string ActiveAsc = "active_asc";
        public static string ActiveDesc = "active_desc";

        public string Search { get; set; }
        public ICollection<string> Language { get; set; } //todo enumerate these
        public string Sort { get; set; }

        public IEnumerable<string> Commitment { get; set; }
        public IEnumerable<string> Size { get; set; }
        public IEnumerable<string> Model { get; set; }
        public IEnumerable<int> Activity { get; set; }

        public IEnumerable<int> RolePlay { get; set; }
        public IEnumerable<int> Recruiting { get; set; }

        public int PageSize { get; set; }
        public int Page { get; set; }

        public SearchRequestInternal(SearchRequest copy) {
            Search = copy.Search;
            Language = copy.Language != null ? new List<string> {copy.Language} : new List<string>();
            Sort = MapSort(copy.Sort);

            PageSize = copy.PageSize;
            Page = copy.Page;

            Commitment = copy.Commitment.Select(MapCommitment);
            Size = MapSize(copy.Size);
            Model = copy.Model.Select(MapArchetype);
            Activity = copy.Activity.Cast<int>();

            RolePlay = MapBoolean(copy.RolePlay);
            Recruiting = MapBoolean(copy.Recruiting);
        }

        private IEnumerable<string> MapSize(ICollection<Size> sizes) {
            if (sizes.Any(s => !Enum.IsDefined(typeof(Size), s)))
                throw new ArgumentOutOfRangeException(nameof(sizes));
            return sizes.Select(s => s.ToString().ToLower());
        }

        private IEnumerable<int> MapBoolean(SearchRequest.Boolean boolean) {
            return boolean == SearchRequest.Boolean.Yes ? new List<int> {1} :
                boolean == SearchRequest.Boolean.No ? new List<int> {0} :
                new List<int>( );
        }

        private string MapCommitment(Commitment commitment) {
            switch (commitment) {
                case Models.Commitment.Regular:
                    return "RE";
                case Models.Commitment.Casual:
                    return "CA";
                case Models.Commitment.Hardcore:
                    return "HA";
            }

            throw new ArgumentOutOfRangeException(nameof(commitment), commitment, null);
        }

        private string MapArchetype(Archetype archetype) {
            switch (archetype) {
                case Archetype.Organization:
                    return "generic";
                case Archetype.Corporation:
                    return "corp";
                case Archetype.Pmc:
                    return "pmc";
                case Archetype.Faith:
                    return "faith";
                case Archetype.Syndicate:
                    return "syndicate";
            }

            throw new ArgumentOutOfRangeException(nameof(archetype), archetype, null);
        }

        private string MapSort(SearchRequest.Sorting sort) {
            switch (sort) {
                case SearchRequest.Sorting.SizeAscending:
                    return SizeAsc;
                case SearchRequest.Sorting.SizeDescending:
                    return SizeDesc;
                case SearchRequest.Sorting.NameAscending:
                    return NameAsc;
                case SearchRequest.Sorting.NameDescending:
                    return NameDesc;
                case SearchRequest.Sorting.CreatedAscending:
                    return CreatedAsc;
                case SearchRequest.Sorting.CreatedDescending:
                    return CreatedDesc;
                case SearchRequest.Sorting.ActiveAscending:
                    return ActiveAsc;
                case SearchRequest.Sorting.ActiveDescending:
                    return ActiveDesc;
            }

            throw new ArgumentOutOfRangeException(nameof(sort), sort, null);
        }
    }
}
/*
empty {"sort":"size_desc","search":"","commitment":[],"roleplay":[],"size":[],"model":[],"activity":[],"language":[],"recruiting":[],"pagesize":12,"page":1}
partial {"sort":"size_desc","search":"","commitment":["CA","RE"],"roleplay":["1","0"],"size":["small","medium"],"model":["generic","corp"],"activity":["9","7"],"language":[],"recruiting":["1","0"],"pagesize":12,"page":1}
full {"sort":"size_desc","search":"test","commitment":["CA","RE","HA"],"roleplay":["1","0"],"size":["small","medium","large"],"model":["generic","corp","pmc","faith","syndicate"],"activity":["9","7","12","6","5","10","4","3","8","2","13","11","1"],"language":["ce"],"recruiting":["1","0"],"pagesize":12,"page":1}
*/