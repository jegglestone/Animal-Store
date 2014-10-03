using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;
using AnimalStore.Model;
using AnimalStore.Web.Repository;
using AnimalStore.Web.ViewModels;
using AnimalStore.Web.Wrappers.Interfaces;

namespace AnimalStore.Web.Controllers
{
  using System.Collections.Generic;
  using System.Linq;

  [OutputCache(CacheProfile = "ControllerOutputCacheProfile")]
  public class SearchController : Controller
  {
    private readonly ISearchAPIFacade _searchRepository;
    private const int _firstPage = 1;
    private readonly IConfiguration _configuration;
    private readonly HttpSessionState _session;
    private readonly ICustomHttpRequestWrapper _httpRequestWrapper;


    public SearchController(
      ISearchAPIFacade searchRepository, 
      HttpSessionState session, 
      ICustomHttpRequestWrapper httpRequestWrapper,
      IConfiguration configuration)
    {
      _searchRepository = searchRepository;
      _session = session;
      _httpRequestWrapper = httpRequestWrapper;
      _configuration = configuration;
    }

    //
    // GET: /Search/Dogs
    [HttpGet]
    public ActionResult Dogs(SearchViewModel searchViewModel)
    {
      PageableResults<Dog> searchResults;
      if (searchViewModel.IsNationalSearch)
        searchResults = HandleNationalDogSearch(searchViewModel);
      else
      {
        if (searchViewModel.PlaceId == 0)
        {
          var places = _searchRepository.GetPlaces(searchViewModel.Location);
          if (places.Count != 1) return AmbiguousLocations(searchViewModel, places);

          searchViewModel.PlaceId = places.First().PlacesID;
        }
        searchResults = HandleLocalDogSearch(
          searchViewModel);
      }

      _session[SessionStoreKeys.SearchViewModel] = searchViewModel;

      if (searchResults == null)
        return RedirectToAction("Index", "Home", searchViewModel); // pass some TempData here to say place not found

      return View("Dogs", searchResults);
    }

    //
    // GET: /Search/DogsSorted
    [HttpGet]
    public ActionResult DogsSorted()
    {
      var searchViewModel = 
        (SearchViewModel) _session[SessionStoreKeys.SearchViewModel];

      if (searchViewModel == null) return RedirectToAction("Index", "Home");

      searchViewModel.SortBy = 
        _httpRequestWrapper.GetQueryStringValueByKey(QuerystringKeys.SortBy);
      searchViewModel.PageNumber = 1;

      return RedirectToAction("Dogs", BuildRouteValuesForDogsSearchViewModel(searchViewModel));
    }

    private ActionResult AmbiguousLocations(SearchViewModel search, List<Place> places)
    {
      return View("AmbiguousLocation", new AmbiguousLocationViewModel
      {
        SearchViewModel = search,
        Places = places
      });
    }

    private static RouteValueDictionary BuildRouteValuesForDogsSearchViewModel(SearchViewModel searchViewModel)
    {
      return new RouteValueDictionary
      {
        {"SelectedBreed", searchViewModel.SelectedBreed},
        {"BreedsSelectList", searchViewModel.BreedsSelectList},
        {"Location", searchViewModel.Location},
        {"SortBy", searchViewModel.SortBy},
        {"PageNumber", searchViewModel.PageNumber},
        {"IsNationalSearch", searchViewModel.IsNationalSearch},
      };
    }

    private PageableResults<Dog> HandleNationalDogSearch(
      SearchViewModel searchViewModel)
    {
      SetPageNumber(searchViewModel);

      var defaultPageSize = _configuration.GetDefaultSearchResultPageSize();

      return !IsSearchingForAnyBreed(searchViewModel.SelectedBreed)
        ? _searchRepository.GetDogsByBreed(
          searchViewModel.PageNumber,
          defaultPageSize,
          searchViewModel.SelectedBreed,
          searchViewModel.SortBy)
        : _searchRepository.GetDogs(
          searchViewModel.PageNumber, defaultPageSize);
    }

    private PageableResults<Dog> HandleLocalDogSearch(
      SearchViewModel searchViewModel)
    {
      SetPageNumber(searchViewModel);

      var defaultPageSize = _configuration.GetDefaultSearchResultPageSize();

      return !IsSearchingForAnyBreed(searchViewModel.SelectedBreed)
        ? _searchRepository.GetDogsByBreedAndLocation(
          searchViewModel.SelectedBreed,
          searchViewModel.PageNumber,
          defaultPageSize,
          searchViewModel.PlaceId,
          searchViewModel.SortBy)
        : _searchRepository.GetDogsByLocation(
          searchViewModel.PageNumber, 
          defaultPageSize,
          searchViewModel.PlaceId,
          searchViewModel.SortBy);
    }

    private static void SetPageNumber(SearchViewModel searchViewModel)
    {
      if (searchViewModel.PageNumber == 0)
        searchViewModel.PageNumber = _firstPage;
    }

    private static bool IsSearchingForAnyBreed(int selectedBreed)
    {
      var isSearchingForAnyBreed = selectedBreed == 0;
      return isSearchingForAnyBreed;
    }
  }
}
