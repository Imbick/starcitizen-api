
namespace Imbick.StarCitizen.Api.Models {
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class OrgsSearchRequest {
        public enum OrganisationSorting {
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

        public OrganisationSorting Sort { get; set; }
        public string Search { get; set; }
        public string Language { get; set; } //todo enumerate these
        public ICollection<OrgCommitment> Commitment { get; set; } = new List<OrgCommitment>();
        public ICollection<OrgSize> Size { get; set; } = new List<OrgSize>();
        public ICollection<OrgArchetype> Model => new List<OrgArchetype>();
        public ICollection<OrgActivity> Activity => new List<OrgActivity>();
        public Boolean RolePlay { get; set; }
        public Boolean Recruiting { get; set; }
        public int PageSize { get; set; } = 12;
        public int Page { get; set; } = 1;
    }

    internal class OrgsSearchRequestInternal {
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

        public OrgsSearchRequestInternal(OrgsSearchRequest copy) {
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

        private IEnumerable<string> MapSize(ICollection<OrgSize> sizes) {
            if (sizes.Any(s => !Enum.IsDefined(typeof(OrgSize), s)))
                throw new ArgumentOutOfRangeException(nameof(sizes));
            return sizes.Select(s => s.ToString().ToLower());
        }

        private IEnumerable<int> MapBoolean(OrgsSearchRequest.Boolean boolean) {
            return boolean == OrgsSearchRequest.Boolean.Yes ? new List<int> {1} :
                boolean == OrgsSearchRequest.Boolean.No ? new List<int> {0} :
                new List<int>( );
        }

        private string MapCommitment(OrgCommitment commitment) {
            switch (commitment) {
                case Models.OrgCommitment.Regular:
                    return "RE";
                case Models.OrgCommitment.Casual:
                    return "CA";
                case Models.OrgCommitment.Hardcore:
                    return "HA";
            }

            throw new ArgumentOutOfRangeException(nameof(commitment), commitment, null);
        }

        private string MapArchetype(OrgArchetype archetype) {
            switch (archetype) {
                case OrgArchetype.Organization:
                    return "generic";
                case OrgArchetype.Corporation:
                    return "corp";
                case OrgArchetype.Pmc:
                    return "pmc";
                case OrgArchetype.Faith:
                    return "faith";
                case OrgArchetype.Syndicate:
                    return "syndicate";
            }

            throw new ArgumentOutOfRangeException(nameof(archetype), archetype, null);
        }

        private string MapSort(OrgsSearchRequest.OrganisationSorting sort) {
            switch (sort) {
                case OrgsSearchRequest.OrganisationSorting.SizeAscending:
                    return SizeAsc;
                case OrgsSearchRequest.OrganisationSorting.SizeDescending:
                    return SizeDesc;
                case OrgsSearchRequest.OrganisationSorting.NameAscending:
                    return NameAsc;
                case OrgsSearchRequest.OrganisationSorting.NameDescending:
                    return NameDesc;
                case OrgsSearchRequest.OrganisationSorting.CreatedAscending:
                    return CreatedAsc;
                case OrgsSearchRequest.OrganisationSorting.CreatedDescending:
                    return CreatedDesc;
                case OrgsSearchRequest.OrganisationSorting.ActiveAscending:
                    return ActiveAsc;
                case OrgsSearchRequest.OrganisationSorting.ActiveDescending:
                    return ActiveDesc;
            }

            throw new ArgumentOutOfRangeException(nameof(sort), sort, null);
        }
    }
}
/*
empty {sort:"size_desc",search:"",commitment:[],roleplay:[],size:[],model:[],activity:[],language:[],recruiting:[],pagesize:12,page:1}
partial {sort:"size_desc",search:"",commitment:["CA","RE"],roleplay:["1","0"],size:["small","medium"],model:["generic","corp"],activity:["9","7"],"language":[],recruiting:["1","0"],pagesize:12,page:1}
full {sort:"size_desc",search:"test",commitment:["CA","RE","HA"],roleplay:["1","0"],size:["small","medium","large"],model:["generic","corp","pmc","faith","syndicate"],activity:["9","7","12","6","5","10","4","3","8","2","13","11","1"],language:["ce"],recruiting:["1","0"],pagesize:12,page:1}
*/