namespace Imbick.StarCitizen.Api {
    using System.Linq;
    using Imbick.StarCitizen.Api.HtmlSerializers;
    using Models;
    using RestSharp;

    public class GetOrgMembersRestRequest
        : RestRequest {
        public static string GetOrgMembersResource = "api/orgs/getOrgMembers";

        public GetOrgMembersRestRequest(OrgMembersSearchRequest searchRequest)
            : base(GetOrgMembersResource) {
            SimpleJson.CurrentJsonSerializerStrategy = new LowerCaseSerializerStrategy();
            AddJsonBody(searchRequest == null ? null : new OrgMembersSearchRequestInternal(searchRequest));
        }
    }
}