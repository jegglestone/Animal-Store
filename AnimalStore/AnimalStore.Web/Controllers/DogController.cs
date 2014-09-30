using AnimalStore.Web.Repository;
using System.Web.Mvc;

namespace AnimalStore.Web.Controllers
{
  using ViewModels;

  [OutputCache(CacheProfile = "ControllerOutputCacheProfile")]
    public class DogController : Controller
    {
        private readonly ISearchAPIFacade _searchRepository;

        public DogController(ISearchAPIFacade searchRepository)
        {
            _searchRepository = searchRepository;
        }

        //
        // GET: /Dog/Details/5

        public ActionResult Details(int id, SearchViewModel searchViewModel)
        {
            // get dog
            var dog = _searchRepository.GetDogDetails(id);

            return View(dog);
        }

        //
        // GET: /Dog/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Dog/Create

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
                return View();
            }
        }

        //
        // GET: /Dog/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Dog/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Dog/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Dog/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
