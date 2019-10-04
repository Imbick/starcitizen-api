namespace Imbick.StarCitizen.Api {
    using System.Linq;
    using Imbick.StarCitizen.Api.HtmlSerializers;
    using Models;
    using RestSharp;

    public class GetOrgsRestRequest
        : RestRequest {
        public static string GetOrgsResource = "api/orgs/getOrgs";

        public GetOrgsRestRequest(OrgsSearchRequest searchRequest)
            : base(GetOrgsResource) {
            SimpleJson.CurrentJsonSerializerStrategy = new LowerCaseSerializerStrategy();
            AddJsonBody(searchRequest == null ? null : new OrgsSearchRequestInternal(searchRequest));
        }
    }
}