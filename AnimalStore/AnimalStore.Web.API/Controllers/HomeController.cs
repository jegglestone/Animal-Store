using System.Web.Mvc;
using AnimalStore.Data.DataContext;

namespace AnimalStore.Web.API.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var dataContext = new DataContext();
            dataContext.Database.Initialize(true);

            return View();
        }
    }
}
