using System.Web.Mvc;
using System.Web.Routing;
using AnimalStore.Model;
using AnimalStore.Web.Repository;
using AnimalStore.Web.ViewModels;

namespace AnimalStore.Web.Controllers
{
    public class SearchController : Controller
    {
        private readonly ISearchAPIFacade _searchRepository;
        private const int _firstPage = 1;
        private const int _defaultPageSize = 25;

        public SearchController(ISearchAPIFacade searchRepository)
        {
            _searchRepository = searchRepository;
        }

        //
        // GET: /Search/Dogs

        [HttpGet]
        public ActionResult Dogs(SearchViewModel searchViewModel)
        {
            PageableResults<Dog> searchResults = null;

            if (searchViewModel.IsNationalSearch)
                searchResults = HandleNationalDogSearch(searchViewModel);

            Session[SessionStoreKeys.SearchViewModel] = searchViewModel;

            return View("Dogs", searchResults);
        }

        [HttpGet]
        public ActionResult DogsSorted()
        {
            var searchViewModel = (SearchViewModel)Session[SessionStoreKeys.SearchViewModel];

            searchViewModel.SortBy = Request.QueryString["sortBy"];

            return RedirectToAction("Dogs", BuildRouteValuesForDogsSearchViewModel(searchViewModel));
        }

        private RouteValueDictionary BuildRouteValuesForDogsSearchViewModel(SearchViewModel searchViewModel)
        {
            return new RouteValueDictionary
                {
                    { "searchViewModel.SelectedBreed", searchViewModel.SelectedBreed },
                    { "searchViewModel.BreedsSelectList", searchViewModel.BreedsSelectList },
                    { "searchViewModel.Location", searchViewModel.Location },
                    { "searchViewModel.SortBy", searchViewModel.SortBy },
                    { "searchViewModel.PageNumber", searchViewModel.PageNumber },
                    { "searchViewModel.IsNationalSearch", searchViewModel.IsNationalSearch },
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
