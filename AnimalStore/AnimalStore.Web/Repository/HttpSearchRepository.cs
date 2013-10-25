using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using AnimalStore.Model;

namespace AnimalStore.Web.Repository
{
    public class HttpSearchRepository : ISearchRepository
    {
        private readonly HttpClient _httpClient;
        private static readonly string API_base_URL = ConfigurationManager.AppSettings["WebAPIUrl"];
        private static readonly string breeds_Url = API_base_URL + "/breeds";

        public HttpSearchRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IList<Breed> GetBreeds()
        {
            //Send a GET WebRequest
            var request = WebRequest.Create(breeds_Url);

            //Receive the Json formatted data
            var response = request.GetResponse();  // also an async method available

            //Deserialize the object(s)
            var jsonSerializer =
                new DataContractJsonSerializer(typeof(List<Breed>));

            //Do something useful with the received data
            var breeds = (List<Breed>)jsonSerializer.ReadObject(response.GetResponseStream());
            //parse json into List<Breed>()

            return breeds;
        }

        public IList<Breed> GetBreedsAsync()
        {
            //// use httpClient to consume Api
            //_httpClient.GetAsync(breeds_Url);


            throw new System.NotImplementedException();
        }

        //public async Task GetAsync(string uri)
        //{
        //    var content = await _httpClient.GetStringAsync(uri);
        //    return await Task.Run(() => JsonObject.Parse(content));  //also see JsonObject
        //}


        private async void ProcessUrlAsync()
        {
            var requestMessage = new HttpRequestMessage() { RequestUri = _httpClient.BaseAddress };
            Task<HttpResponseMessage> getTask = _httpClient.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead);
        }
    }
}