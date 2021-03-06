﻿namespace AnimalStore.Web.Repository
{
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Net;
  using Common.Helpers;
  using Model;
  using Helpers;
  using Wrappers.Interfaces;
  using Factories;
  using System.Web.Http;

  public class SearchAPIFacade : ISearchAPIFacade
  {
    private string _API_base_URL
    {
      get { return _configuration.GetWebAPIUrl(); }
    }

    private string _breeds_Url
    {
      get { return _API_base_URL + "/breeds"; }
    }

    private string _dogs_Url
    {
      get { return _API_base_URL + "/dogs"; }
    }

    private string _places_Url
    {
      get { return _API_base_URL + "/places";  }
    }

    private IList<Breed> _breeds;
    private PageableResults<Dog> _dogs;
    private readonly IExceptionHelper _exceptionHelper;
    private readonly IDataContractJsonSerializerWrapper _dataContractJsonSerializerWrapper;
    private readonly IConfiguration _configuration;
    private readonly IWebAPIRequestWrapper _webAPIRequestWrapper;
    private readonly IResponseStreamHelper _responseStreamHelper;

    public SearchAPIFacade(
      IDataContractJsonSerializerWrapper dataContractJsonSerializerWrapper,
      IExceptionHelper exceptionHelper,
      IConfiguration configuration,
      IWebAPIRequestWrapper webAPIRequestWrapper,
      IResponseStreamHelper responseStreamHelper)
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
          var apiResponseData =
            DeserializeAPIResponseData(stream, typeof (List<Breed>));
          if (apiResponseData != null)
          {
            _breeds = (List<Breed>) apiResponseData;
          }
        }
      }
      catch (Exception e)
      {
        _exceptionHelper.HandleException(
          "Response from Breeds service resulted in an error in GetBreeds()", e, (typeof (SearchAPIFacade)));
      }
      finally
      {
        DisposeOfWebResponse(response);
      }

      return _breeds;
    }

    public PageableResults<Dog> GetDogs(int page, int pageSize)
    {
      var url =string.Format("{0}?page={1}&pageSize={2}&format=json", 
          _dogs_Url, page, pageSize) ;

      return GetDogsByResponse(url);
    }

    public PageableResults<Dog> GetDogsByBreed(
      int page, int pageSize, int breedId, string sortBy = null)
    {
      var url = sortBy != null
        ? string.Format("{0}{1}?page={2}&pageSize={3}&breedid={4}&sortBy={5}&format=json"
          , _dogs_Url, "/breed", page, pageSize, breedId, sortBy)
        : string.Format("{0}{1}?page={2}&pageSize={3}&breedid={4}&format=json"
          , _dogs_Url, "/breed", page, pageSize, breedId);

      return GetDogsByResponse(url);
    }

    public List<Place> GetPlaces(string location)
    {
      var places = new List<Place>();

      var placeLookupUrl = string.Format(
        "{0}?location={1}", _places_Url, location);
      var placeLookupResponse =
        _webAPIRequestWrapper.GetResponse(placeLookupUrl);

      try
      {
        using (var stream =
          _responseStreamHelper.GetResponseStream(placeLookupResponse))
        {
          var apiResponseData =
            DeserializeAPIResponseData(stream, typeof (List<Place>));

          if (apiResponseData != null)
          {
            places = (List<Place>)apiResponseData;
          }
        }
      }
      catch (HttpResponseException)
      {
        places = null;
      }
      catch (Exception e)
      {
        _exceptionHelper.HandleException(
          "Response from places service resulted in an error in GetDogsByBreedAndLocation()",
          e,
          (typeof(SearchAPIFacade)));
      }
      finally
      {
        DisposeOfWebResponse(placeLookupResponse);
      }
      return places;
    }

    public PageableResults<Dog> GetDogsByLocation(int page, int pageSize, int placeId, string sortBy = null)
    {
      var url = sortBy != null
        ? string.Format("{0}?page={1}&pageSize={2}&placeId={3}&sortBy={4}&format=json",
          _dogs_Url, page, pageSize, placeId, sortBy)
        : string.Format("{0}?page={1}&pageSize={2}&placeId={3}&format=json",
          _dogs_Url, page, pageSize, placeId);

      return GetDogsByResponse(url);
    }

    public PageableResults<Dog> GetDogsByBreedAndLocation(int breedId, int page, int pageSize, int placeId, string sortBy = null)
    {
      var url = sortBy != null
        ? string.Format("{0}{1}?page={2}&pageSize={3}&breedid={4}&placeId={5}&sortBy={6}&format=json",
          _dogs_Url, "/breed", page, pageSize, breedId, placeId, sortBy)
        : string.Format("{0}{1}?page={2}&pageSize={3}&breedid={4}&placeId={5}&format=json",
          _dogs_Url, "/breed", page, pageSize, breedId, placeId);

      return GetDogsByResponse(url);
    }

    // GET /api/dogs?breedid=1&page=1&pagesize=100&placeId=1&format=json
    // GET /api/dogs?breedid=4&page=1&pagesize=30&placeid=12472&format=json 
    // GET /api/dogs/breed?breedid=67&page=1&pagesize=30&format=json         
    // GET /api/dogs/breed?breedid=7&page=1&pagesize=30&format=json         

    public Dog GetDogDetails(int id)
    {
      var dog = new Dog();
      var response = _webAPIRequestWrapper.GetResponse(
        string.Format("{0}/{1}", _dogs_Url, id));
      try
      {
        using (var stream = _responseStreamHelper.GetResponseStream(response))
        {
          var apiResponseData =
            DeserializeAPIResponseData(stream, typeof(Dog));
          if (apiResponseData != null)
          {
            dog = (Dog) apiResponseData;
          }
        }
      }
      catch (HttpResponseException)
      {
        dog = null;
      }
      catch (Exception e)
      {
        _exceptionHelper.HandleException(
          "Response from Dogs service resulted in an error in GetDogs()", 
          e, 
          (typeof (SearchAPIFacade)));
      }
      finally
      {
        DisposeOfWebResponse(response);
      }

      return dog;
    }

    private PageableResults<Dog> GetDogsByResponse(string url)
    {
      var response = _webAPIRequestWrapper.GetResponse(url);

      try
      {
        using (var stream = _responseStreamHelper.GetResponseStream(response))
        {
          var apiResponseData = 
            DeserializeAPIResponseData(stream, typeof (PageableResults<Dog>));
          if (apiResponseData != null)
          {
            _dogs = (PageableResults<Dog>) apiResponseData;
          }
        }
      }
      catch (Exception e)
      {
        _exceptionHelper.HandleException(
          "Response from Dogs service resulted in an error in GetDogs()", 
          e, 
          (typeof (SearchAPIFacade)));
      }
      finally
      {
        DisposeOfWebResponse(response);
      }

      return _dogs;
    }

    private object DeserializeAPIResponseData(Stream stream, Type type)
    {
      return _dataContractJsonSerializerWrapper.ReadObject(stream,
        DataContractJsonSerializerFactory.GetDataContractJsonSerializer(type));
    }

    private static void DisposeOfWebResponse(WebResponse response)
    {
      if (response == null) return;
      response.Close();
      response.Dispose();
    }
  }
}