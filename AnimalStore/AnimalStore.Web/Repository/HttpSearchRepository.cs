using System;
using System.Collections.Generic;
using System.Net;
using AnimalStore.Common.Helpers;
using AnimalStore.Model;
using AnimalStore.Web.Facades.Interfaces;
using AnimalStore.Web.Factories;
using AnimalStore.Web.Helpers.Interfaces;

namespace AnimalStore.Web.Repository
{
    public class HttpSearchRepository : ISearchRepository
    {
        private string _API_base_URL {
            get { return _configuration.GetWebAPIUrl(); }
        }

        private string _breeds_Url {
            get { return _API_base_URL + "/breeds"; }
        }

        private string _dogs_Url {
            get { return _API_base_URL + "/dogs"; }
        }

        private IList<Breed> _breeds;
        private PageableResults<Dog> _dogs; 
        private readonly IExceptionHelper _exceptionHelper;
        private readonly IDataContractJsonSerializerWrapper _dataContractJsonSerializerWrapper;
        private readonly IConfiguration _configuration;
        private readonly IWebAPIRequestWrapper _webAPIRequestWrapper;
        private readonly IResponseStreamHelper _responseStreamHelper;

        public HttpSearchRepository(IDataContractJsonSerializerWrapper dataContractJsonSerializerWrapper,
            IExceptionHelper exceptionHelper, IConfiguration configuration, IWebAPIRequestWrapper webAPIRequestWrapper, IResponseStreamHelper responseStreamHelper)
        {
            _dataContractJsonSerializerWrapper = dataContractJsonSerializerWrapper;
            _exceptionHelper = exceptionHelper;
            _configuration = configuration;
            _webAPIRequestWrapper = webAPIRequestWrapper;
            _responseStreamHelper = responseStreamHelper;
            _breeds = new List<Breed>();
            _dogs = new PageableResults<Dog>();
        }

        public IList<Breed> GetBreeds()
        {
            var response = _webAPIRequestWrapper.GetResponse(_breeds_Url);
            try
            {
                using (var stream = _responseStreamHelper.GetResponseStream(response))
                {
                    var apiResponseData = _dataContractJsonSerializerWrapper.ReadObject(stream, DataContractJsonSerializerFactory.GetDataContractJsonSerializer(typeof(List<Breed>)));
                    if (apiResponseData != null)
                    {
                        _breeds = (List<Breed>)apiResponseData;
                    }
                }
            }
            catch (Exception e)
            {
                _exceptionHelper.HandleException("Response from Breeds service resulted in an error in GetBreeds()", e, (typeof (HttpSearchRepository)));
            }
            finally
            {
                DisposeOfWebResponse(response);
            }

            return _breeds;
        } 

        public PageableResults<Dog> GetDogs(int page, int pageSize)
        {
            var response = _webAPIRequestWrapper.GetResponse(string.Format("{0}?page={1}&pageSize={2}&format=json", _dogs_Url, page, pageSize));
            return GetDogsByResponse(response);
        }

        // TODO: Unit test
        public PageableResults<Dog> GetDogs(int page, int pageSize, int breedId)
        {
            var response = _webAPIRequestWrapper.GetResponse(string.Format("{0}?page={1}&pageSize={2}&breedid={3}&format=json", _dogs_Url + "/breed", page, pageSize, breedId));
            return GetDogsByResponse(response);
        }

        private PageableResults<Dog> GetDogsByResponse(WebResponse response)
        {
            try
            {
                using (var stream = _responseStreamHelper.GetResponseStream(response))
                {
                    var apiResponseData = _dataContractJsonSerializerWrapper.ReadObject(stream, DataContractJsonSerializerFactory.GetDataContractJsonSerializer(typeof(PageableResults<Dog>)));
                    if (apiResponseData != null)
                    {
                        _dogs = (PageableResults<Dog>)apiResponseData;
                    }
                }
            }
            catch (Exception e)
            {
                _exceptionHelper.HandleException("Response from Dogs service resulted in an error in GetDogs()", e, (typeof(HttpSearchRepository)));
            }
            finally
            {
                DisposeOfWebResponse(response);
            }

            return _dogs;
        }

        private static void DisposeOfWebResponse(WebResponse response)
        {
            if (response == null) return;
            response.Close();
            response.Dispose();
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