using System;
using System.Net.Http;
using System.Web.Mvc;
using AnimalStore.Common.Constants;
using AnimalStore.Web.Repository;
using AnimalStore.Web.ViewModels;

namespace AnimalStore.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly SearchViewModel _searchViewModel;
        private readonly ISearchRepository _searchRepository;

        public HomeController(SearchViewModel searchViewModel, ISearchRepository searchRepository)
        {
            _searchViewModel = searchViewModel;
            _searchRepository = searchRepository;
        }

        //TODO: Unit test!
        public ActionResult Index()
        {
            ViewBag.Message = "Your first stop for finding and advertising dogs in the UK.";

            _searchViewModel.Breeds =_searchRepository.GetBreeds();

            // TODO: Inject
            var searchRepoTMP = new HttpSearchRepository(new HttpClient());

            return View(_searchViewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Thanks for visiting. Here at " + SiteNames.DOGSTORE_SITE_NAME + ", we are dedicated to helping you find the ideal pet for you or you family or the ideal new home for your existing animals.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Get in touch.";

            return View();
        }

        public ActionResult Find()
        {
            // TODO: perform siteMap search, animal search, service search and breeder search

            throw new NotImplementedException();
        }
    }
}
