using System.Web.Mvc;
using AnimalStore.Model;
using AnimalStore.Web.Repository;
using AnimalStore.Web.ViewModels;

namespace AnimalStore.Web.Controllers
{
    public class SearchController : Controller
    {
        private readonly ISearchRepository _searchRepository;
        private const int _defaultPageSize = 25;

        public SearchController(ISearchRepository searchRepository)
        {
            _searchRepository = searchRepository;
        }

        //
        // GET: /Search/Dogs

        [HttpGet]
        public ActionResult Dogs(SearchViewModel viewModel)
        {
            PageableResults<Dog> searchResults = null;

            if (viewModel.IsNationalSearch)
                searchResults = HandleNationalDogSearch(viewModel);

            return View(searchResults);
        }

        private PageableResults<Dog> HandleNationalDogSearch(SearchViewModel viewModel)
        {
            if (!IsSearchingForAnyBreed(viewModel.SelectedBreed))
                return _searchRepository.GetDogs(1, _defaultPageSize, viewModel.SelectedBreed);
            return _searchRepository.GetDogs(1, _defaultPageSize);
        }

        private static bool IsSearchingForAnyBreed(int selectedBreed)
        {
            var isSearchingForAnyBreed = selectedBreed == 0;
            return isSearchingForAnyBreed;
        }
    }
}
