
namespace Imbick.StarCitizen.Api.Models {
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class OrgMembersSearchRequest {
        public enum MembershipType
        {
            Any,
            MainOrg,
            Affiliate
        }

        public string Symbol { get; set; }
        public string Search { get; set; }
        public int? Rank { get; set; } = null;
        public int? Role { get; set; } = null;
        public MembershipType Membership { get; set; } = MembershipType.Any;
        public int PageSize { get; set; } = 32;
        public int Page { get; set; } = 1;
    }

    internal class OrgMembersSearchRequestInternal {
        public string Symbol { get; set; }
        public string Search { get; set; }
        public int? Rank { get; set; }
        public int? Role { get; set; }

        [RestSharp.Deserializers.DeserializeAs(Name = "main_org")]
        [RestSharp.Serializers.SerializeAs(Name = "main_org", Attribute = true, Content = true, NameStyle = RestSharp.Serializers.NameStyle.LowerCase)]
        public int? Main_Org { get; set; }

        public int PageSize { get; set; }
        public int Page { get; set; }

        public OrgMembersSearchRequestInternal(OrgMembersSearchRequest copy) {
            Symbol = copy.Symbol;
            Search = copy.Search;
            Rank = copy.Rank;
            Role = copy.Role;
            Main_Org = MapMembershipType(copy.Membership);
            PageSize = copy.PageSize;
            Page = copy.Page;
        }

        private int? MapMembershipType(OrgMembersSearchRequest.MembershipType membershipType)
        {
            return membershipType == OrgMembersSearchRequest.MembershipType.MainOrg ? 1 :
                membershipType == OrgMembersSearchRequest.MembershipType.Affiliate ? 0 :
                (int?)null;
        }
    }
}
/*
minimal {symbol:"SWISS",search:"",pagesize:32,page:1}
search name {symbol:"SWISS",search:"Meetsch"}
filter rank {symbol:"SWISS",search:"",rank:1}
filter role {symbol:"SWISS",search:"",role:1}
filter main_org {symbol:"SWISS",search:"",main_org:1}
filter affiliate {symbol:"SWISS",search:"",main_org:0}
full {symbol:"SWISS",search:"Looping",rank:1,role:1,main_org:1,pagesize:32,page:1}
*/
