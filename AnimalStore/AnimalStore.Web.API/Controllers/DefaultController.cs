using System.Web.Mvc;

namespace AnimalStore.Web.API.Controllers
{
    public class DefaultController : Controller
    {
        public ActionResult Index()
        {
            var logManager = new Common.Logging.LogManager();
            var log = logManager.GetLogger((typeof (DefaultController)));
            log.Info("logging from default Controller");

            return View();
        }
    }
}
