
namespace Imbick.StarCitizen.Api {
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;
    using RestSharp;

    public class OrganisationsClient {

        public OrganisationsClient()
            : this("https://robertsspaceindustries.com/") { }

        public OrganisationsClient(string baseUrl)
            : this(new Uri(baseUrl)) { }

        public OrganisationsClient(Uri baseUrl)
            : this(new RestClient(baseUrl)) { }

        public OrganisationsClient(IRestClient client) {
            _client = client;
        }

        public async Task<IEnumerable<Organisation>> ListAsync() {
            return await GetAsync();
        }

        public async Task<IEnumerable<Organisation>> GetAsync(SearchRequest searchRequest = null) {
            var request = new GetOrgsRestRequest(searchRequest);
            try {
                var response = await _client.ExecutePostTaskAsync<Response>(request);
                if (!response.IsSuccessful)
                    throw new Exception($"Request to endpoint was unsuccessful. {response.ErrorMessage}");
                var decodedHtml = response.Data.Data.Html.Replace("\\\"", "\""); //todo properly decode this
                return _deserialiser.DeserialiseList(decodedHtml);
            } catch (Exception e) {
                throw new Exception("Problem retrieving orgs. See inner exception for details.", e);
            }
        }

        private readonly IRestClient _client;
        private readonly OrganisationHtmlSerialiser _deserialiser = new OrganisationHtmlSerialiser();
    }
}