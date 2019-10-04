[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Imbick.StarCitizen.Api.Tests")]
namespace Imbick.StarCitizen.Api {
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Imbick.StarCitizen.Api.HtmlSerializers;
    using Models;
    using RestSharp;

    public class RsiApiClient {
        private readonly IRestClient _client;

        public RsiApiClient()
            : this("https://robertsspaceindustries.com/") { }

        public RsiApiClient(string baseUrl)
            : this(new Uri(baseUrl)) { }

        public RsiApiClient(Uri baseUrl)
            : this(new RestClient(baseUrl)) { }

        public RsiApiClient(IRestClient client) {
            _client = client;
        }

        #region getOrgs
        public async Task<IEnumerable<Org>> ListOrgsAsync() {
            return await GetOrgsAsync();
        }

        public async Task<IEnumerable<Org>> GetOrgsAsync(OrgsSearchRequest searchRequest = null) {
            var request = new GetOrgsRestRequest(searchRequest);
            try {
                var response = await _client.ExecutePostTaskAsync<Response>(request);
                if (!response.IsSuccessful)
                    throw new Exception($"Request to endpoint was unsuccessful. {response.ErrorMessage}");
                var decodedHtml = response.Data.Data.Html.Replace("\\\"", "\"");
                return _orgDeserialiser.DeserializeList(decodedHtml);
            } catch (Exception e) {
                throw new Exception("Problem retrieving orgs. See inner exception for details.", e);
            }
        }

        private readonly OrgHtmlSerializer _orgDeserialiser = new OrgHtmlSerializer();
        #endregion

        #region getOrgMembers
        public async Task<IEnumerable<OrgMember>> ListOrgMembersAsync(string orgSymbol, string memberSearch = null)
        {
            return await GetOrgMembersAsync(new OrgMembersSearchRequest() { Symbol = orgSymbol, Search = memberSearch });
        }

        public async Task<IEnumerable<OrgMember>> GetOrgMembersAsync(OrgMembersSearchRequest searchRequest = null)
        {
            var request = new GetOrgMembersRestRequest(searchRequest);
            try
            {
                var response = await _client.ExecutePostTaskAsync<Response>(request);
                if (!response.IsSuccessful)
                    throw new Exception($"Request to endpoint was unsuccessful. {response.ErrorMessage}");
                var decodedHtml = response.Data.Data.Html.Replace("\\\"", "\"");
                return _orgMemberDeserializer.DeserializeList(decodedHtml);
            }
            catch (Exception e)
            {
                throw new Exception("Problem retrieving org members. See inner exception for details.", e);
            }
        }

        private readonly OrgMemberHtmlSerializer _orgMemberDeserializer = new OrgMemberHtmlSerializer();
        #endregion
    }
}