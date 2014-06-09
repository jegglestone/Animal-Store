using System.Configuration;
using System.Web.Mvc;
using AnimalStore.Common.Constants;
using AnimalStore.Web.Repository;
using AnimalStore.Web.ViewModels;

namespace AnimalStore.Web.Controllers
{
    [OutputCache(CacheProfile = "ControllerOutputCacheProfile")]
    public class HomeController : Controller
    {
        private readonly SearchViewModel _searchViewModel;
        private readonly ISearchAPIFacade _searchRepository;
        private readonly ContactInformation _contactInformation;

        public HomeController(SearchViewModel searchViewModel, ISearchAPIFacade searchRepository, ContactInformation contactInformation)
        {
            _searchViewModel = searchViewModel;
            _searchRepository = searchRepository;
            _contactInformation = contactInformation;
        }

        public ViewResult Index(SearchViewModel searchViewModel)
        {
            ViewBag.Message = ConfigurationManager.AppSettings[AppSettingKeys.ApplicationMainSlogan];

            PopulateBreedSelectList(searchViewModel);

            return View(_searchViewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = string.Format(
                ConfigurationManager.AppSettings[AppSettingKeys.AboutPageMainSlogan], 
                    SiteNames.DOGSTORE_SITE_NAME);

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = ConfigurationManager.AppSettings[AppSettingKeys.ContactPageMainSlogan];

            return View(_contactInformation);
        }

        public ActionResult Find()
        {
            throw new System.NotImplementedException();
        }

        private void PopulateBreedSelectList(SearchViewModel searchViewModel)
        {
            if (searchViewModel == null)
            {
                _searchViewModel.BreedsSelectList = new SelectList(_searchRepository.GetBreeds(), "id", "name");
                return;
            }

            _searchViewModel.BreedsSelectList = searchViewModel.BreedsSelectList 
                ?? new SelectList(_searchRepository.GetBreeds(), "id", "name");
        }
    }
}
