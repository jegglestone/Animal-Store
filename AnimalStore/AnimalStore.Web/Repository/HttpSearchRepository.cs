using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Runtime.Serialization.Json;
using AnimalStore.Model;
using AnimalStore.Web.Helpers;

namespace AnimalStore.Web.Repository
{
    // TODO: Unit test
    public class HttpSearchRepository : ISearchRepository
    {
        private static readonly string _API_base_URL = ConfigurationManager.AppSettings["WebAPIUrl"];
        private static readonly string _breeds_Url = _API_base_URL + "/breeds";

        private readonly IExceptionHelper _exceptionHelper;
        private readonly DataContractJsonSerializer _dataContractJsonSerializer;

        private IList<Breed> _breeds; 

        public HttpSearchRepository(DataContractJsonSerializer dataContractJsonSerializer, IList<Breed> breeds, IExceptionHelper exceptionHelper)
        {
            _dataContractJsonSerializer = dataContractJsonSerializer;
            _breeds = breeds;
            _exceptionHelper = exceptionHelper;
        }

        public IList<Breed> GetBreeds()
        {
            var request = WebRequest.Create(_breeds_Url);
            var response = GetResponse(request);

            try
            {
                _breeds = (List<Breed>) _dataContractJsonSerializer.ReadObject(response.GetResponseStream());
            }
            catch (Exception e)
            {
                _exceptionHelper.HandleException("Response from Breeds service resulted in an error", e, (typeof (HttpSearchRepository)));
            }
            finally
            {
                DisposeOfWebResponse(response);
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
                _exceptionHelper.HandleException("Web Service not available", e, (GetType()));
            }

            return response;
        }

        private void DisposeOfWebResponse(WebResponse response)
        {
            if (response != null)
            {
                response.Close();
                response.Dispose();
            }
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