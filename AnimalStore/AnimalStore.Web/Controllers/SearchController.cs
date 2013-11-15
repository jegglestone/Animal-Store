using System.Web.Mvc;
using AnimalStore.Web.Repository;
using AnimalStore.Web.ViewModels;

namespace AnimalStore.Web.Controllers
{
    public class SearchController : Controller
    {
        private readonly SearchViewModel _searchViewModel;
        private readonly ISearchRepository _searchRepository;

        public SearchController(SearchViewModel searchViewModel, ISearchRepository searchRepository)
        {
            _searchViewModel = searchViewModel;
            _searchRepository = searchRepository;
        }

        //
        // GET: /Search/

        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Search/DogSearchResults

        [HttpGet]
        public ActionResult DogSearch(SearchViewModel viewModel)
        {
            // get dogs from database by calling a repo that calls the API (and use TDD)

            // build some kind of list<dog> model

            // redirect to a view result
            return RedirectToAction("DogSearchResults", "Search");
        }

        //TODO: Move to searchresults controller
        public ViewResult DogSearchResults()
        {
            return View();
        }
    }
}
