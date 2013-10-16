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
            _searchViewModel.Breeds = _searchRepository.GetBreeds();

            return PartialView("_TabbedSearchPartial", _searchViewModel);
        }

        //
        // POST: /Search/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View("_TabbedSearchPartial");
            }
        }
    }
}
