using System.Web.Mvc;
using AnimalStore.Data.DataContext;

namespace AnimalStore.Web.API.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var dataContext = new AnimalsDataContext();
            dataContext.Database.Initialize(true);

            var logManager = new Common.Logging.LogManager();
            var log = logManager.GetLogger((typeof (HomeController)));

            log.Info("We're logging with Log4net");

            return View();
        }
    }
}
