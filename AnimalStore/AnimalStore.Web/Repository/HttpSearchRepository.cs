using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using AnimalStore.Common.Logging;
using AnimalStore.Model;

namespace AnimalStore.Web.Repository
{
    public class HttpSearchRepository : ISearchRepository
    {
        private static readonly string API_base_URL = ConfigurationManager.AppSettings["WebAPIUrl"];
        private static readonly string breeds_Url = API_base_URL + "/breeds";

        public HttpSearchRepository()
        {
        }

        // abstract come of this into a generic
        public List<Breed> GetBreeds()
        {
            //Send a GET WebRequest
            var request = WebRequest.Create(breeds_Url);

            //Receive the Json formatted data
            var response = request.GetResponse();

            //Deserialize the object(s)
            var jsonSerializer =
                new DataContractJsonSerializer(typeof(List<Breed>));

            var breeds = new List<Breed>();
            try
            {
                breeds = (List<Breed>)jsonSerializer.ReadObject(response.GetResponseStream());
            }
            catch (Exception e)
            {
                var logManager = new LogManager();
                var log = logManager.GetLogger((typeof(HttpSearchRepository)));

                log.Error("Response from Breeds service resulted in an error", e);
            }

            return breeds;
        }

        //public IList<Breed> GetBreedsAsync()
        //{
        //    //// use httpClient to consume Api
        //    //_httpClient.GetAsync(breeds_Url);


        //    throw new System.NotImplementedException();
        //}

        //public async Task GetAsync(string uri)
        //{
        //    var content = await _httpClient.GetStringAsync(uri);
        //    return await Task.Run(() => JsonObject.Parse(content));  //also see JsonObject
        //}

        //private async void ProcessUrlAsync()
        //{
        //    var requestMessage = new HttpRequestMessage() { RequestUri = _httpClient.BaseAddress };
        //    Task<HttpResponseMessage> getTask = _httpClient.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead);
        //}
    }
}