using System.Web.Mvc;
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
        public ActionResult Dogs(SearchViewModel viewModel, string sortBy = null)
        {
            PageableResults<Dog> searchResults = null;
            TempData["searchViewModel"] = viewModel;
            
            if (viewModel.IsNationalSearch)
                searchResults = HandleNationalDogSearch(viewModel);

            return View(searchResults);
        }

        [HttpGet]
        public ActionResult Dogs(string sortBy)
        {
            Dogs((SearchViewModel)TempData["searchViewModel"], sortBy);

            return null;
        }

        private PageableResults<Dog> HandleNationalDogSearch(SearchViewModel viewModel)
        {
            if (viewModel.pageNumber == 0) viewModel.pageNumber = _firstPage;

            if (!IsSearchingForAnyBreed(viewModel.SelectedBreed))
                return _searchRepository.GetDogs(viewModel.pageNumber, _defaultPageSize, viewModel.SelectedBreed);
            return _searchRepository.GetDogs(viewModel.pageNumber, _defaultPageSize);
        }

        private static bool IsSearchingForAnyBreed(int selectedBreed)
        {
            var isSearchingForAnyBreed = selectedBreed == 0;
            return isSearchingForAnyBreed;
        }
    }
}
