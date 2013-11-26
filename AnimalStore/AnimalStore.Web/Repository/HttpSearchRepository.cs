using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Runtime.Serialization.Json;
using AnimalStore.Common.Logging;
using AnimalStore.Model;

namespace AnimalStore.Web.Repository
{
    public class HttpSearchRepository : ISearchRepository
    {
        private static readonly string API_base_URL = ConfigurationManager.AppSettings["WebAPIUrl"];
        private static readonly string breeds_Url = API_base_URL + "/breeds";

        private readonly LogManager _logManager;
        private readonly DataContractJsonSerializer _dataContractJsonSerializer;

        private IList<Breed> _breeds; 

        public HttpSearchRepository(
            LogManager logManager
            , DataContractJsonSerializer dataContractJsonSerializer
            , IList<Breed> breeds
            )
        {
            _logManager = logManager;
            _dataContractJsonSerializer = dataContractJsonSerializer;
            _breeds = breeds;
        }

        public IList<Breed> GetBreeds()
        {
            var request = WebRequest.Create(breeds_Url);
            var response = GetResponse(request);

            try
            {
                if (response != null) 
                    return _breeds = (List<Breed>)_dataContractJsonSerializer.ReadObject(response.GetResponseStream());
            }
            catch (Exception e)
            {
                //TODO: aspect/cross cutting concern - look into having a filter attribute
                var log = _logManager.GetLogger((typeof(HttpSearchRepository)));
                log.Error("Response from Breeds service resulted in an error", e);
            }

            return _breeds;
        }

        private WebResponse GetResponse(WebRequest request)
        {
            WebResponse response = null;
            try
            {
                response = request.GetResponse();
            }
            catch (WebException e)
            {
                var log = _logManager.GetLogger((typeof(HttpSearchRepository)));
                log.Error("Web Service not available", e);
            }

            return response;
        }

        #region async stuff
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
        #endregion
    }
}