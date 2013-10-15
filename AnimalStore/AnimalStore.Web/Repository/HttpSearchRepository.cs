using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AnimalStore.Web.Repository
{
    public class HttpSearchRepository : ISearchRepository
    {
        private readonly HttpClient _httpClient;

        public HttpSearchRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IList<Model.Breed> GetBreeds()
        {
            // use httpClient to consume Api
            _httpClient.GetAsync("www.google.com");

        
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