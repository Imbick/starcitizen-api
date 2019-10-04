namespace Imbick.StarCitizen.Api {
    using System.Linq;
    using Models;
    using RestSharp;

    public class GetOrgsRestRequest
        : RestRequest {
        public static string GetOrgsResource = "api/orgs/getOrgs";

        public GetOrgsRestRequest(SearchRequest searchRequest)
            : base(GetOrgsResource) {
            SimpleJson.CurrentJsonSerializerStrategy = new CamelCaseSerializerStrategy();
            AddJsonBody(searchRequest == null ? null : new SearchRequestInternal(searchRequest));
        }
    }

    internal class CamelCaseSerializerStrategy
        : PocoJsonSerializerStrategy {
        protected override string MapClrMemberNameToJsonFieldName(string clrPropertyName) {
            return char.ToLower(clrPropertyName[0]) + clrPropertyName.Substring(1);
        }
    }
}