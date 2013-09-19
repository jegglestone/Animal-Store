using System.Web.Mvc;

namespace AnimalStore.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Your first stop for finding and advertising dogs in the UK.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Thanks for visiting. Here at xxx, we are dedicated to helping you find the ideal pet for you or you family or the ideal new home for your existing animals.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Get in touch.";

            return View();
        }


    }
}
