﻿using System.Collections.Generic;
using System.Web.Mvc;
using AnimalStore.Model;
using AnimalStore.Web.Repository;
using AnimalStore.Web.ViewModels;

namespace AnimalStore.Web.Controllers
{
    public class SearchController : Controller
    {
        private readonly ISearchRepository _searchRepository;

        public SearchController(ISearchRepository searchRepository)
        {
            _searchRepository = searchRepository;
        }

        //
        // GET: /Search/DogSearchResults

        [HttpGet]
        public ActionResult DogSearch(SearchViewModel viewModel)
        {
            PageableResults<Dog> searchResults = null;

            if (viewModel.IsNationalSearch)
                searchResults = _searchRepository.GetDogs(1, 25);

            return RedirectToAction("DogSearchResults", "SearchResults", new { searchResults });
        }

        //TODO: Move to searchresults controller and return SearchResults ViewModel??
        public ViewResult DogSearchResults()
        {
            return View();
        }
    }
}
