﻿using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;
using AnimalStore.Model;
using AnimalStore.Web.Repository;
using AnimalStore.Web.ViewModels;
using AnimalStore.Web.Wrappers.Interfaces;

namespace AnimalStore.Web.Controllers
{
    public class SearchController : Controller
    {
        private readonly ISearchAPIFacade _searchRepository;
        private const int _firstPage = 1;
        private const int _defaultPageSize = 25;
        private readonly HttpSessionState _session;
        private readonly ICustomHttpRequestWrapper _httpRequestWrapper;

        public SearchController(ISearchAPIFacade searchRepository, HttpSessionState session, ICustomHttpRequestWrapper httpRequestWrapper)
        {
            _searchRepository = searchRepository;
            _session = session;
            _httpRequestWrapper = httpRequestWrapper;
        }

        //
        // GET: /Search/Dogs
        [HttpGet]
        public ActionResult Dogs(SearchViewModel searchViewModel)
        {
            PageableResults<Dog> searchResults = null;

            if (searchViewModel.IsNationalSearch)
                searchResults = HandleNationalDogSearch(searchViewModel);

            _session[SessionStoreKeys.SearchViewModel] = searchViewModel;

            return View("Dogs", searchResults);
        }

        //
        // GET: /Search/DogsSorted
        [HttpGet]
        public ActionResult DogsSorted()
        {
            var searchViewModel = (SearchViewModel)_session[SessionStoreKeys.SearchViewModel];

            if (searchViewModel == null) return RedirectToAction("Index", "Home");

            searchViewModel.SortBy = _httpRequestWrapper.GetQueryStringValueByKey(QuerystringKeys.SortBy);
            searchViewModel.PageNumber = 1;

            return RedirectToAction("Dogs", BuildRouteValuesForDogsSearchViewModel(searchViewModel));
        }

        private static RouteValueDictionary BuildRouteValuesForDogsSearchViewModel(SearchViewModel searchViewModel)
        {
            return new RouteValueDictionary
                {
                    { "SelectedBreed", searchViewModel.SelectedBreed },
                    { "BreedsSelectList", searchViewModel.BreedsSelectList },
                    { "Location", searchViewModel.Location },
                    { "SortBy", searchViewModel.SortBy },
                    { "PageNumber", searchViewModel.PageNumber },
                    { "IsNationalSearch", searchViewModel.IsNationalSearch },
                };
        }

        private PageableResults<Dog> HandleNationalDogSearch(SearchViewModel viewModel)
        {
            if (viewModel.PageNumber == 0) viewModel.PageNumber = _firstPage;

            return !IsSearchingForAnyBreed(viewModel.SelectedBreed)
                ? _searchRepository.GetDogs(viewModel.PageNumber, _defaultPageSize, viewModel.SelectedBreed, viewModel.SortBy) 
                : _searchRepository.GetDogs(viewModel.PageNumber, _defaultPageSize);
        }

        private static bool IsSearchingForAnyBreed(int selectedBreed)
        {
            var isSearchingForAnyBreed = selectedBreed == 0;
            return isSearchingForAnyBreed;
        }
    }
}
