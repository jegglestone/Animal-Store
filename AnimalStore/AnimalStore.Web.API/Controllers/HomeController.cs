using System.Web.Mvc;
using AnimalStore.Data.DataContext;
using System;

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

            // TODO:move into unit test and mock up the eventlog etc.
            log.Info("We're logging with Log4net 1");
            log.Info("We're logging with Log4net 2");
            log.Info("We're logging with Log4net 3");
            log.Info("We're logging with Log4net 4");
            log.Info("We're logging with Log4net 5");
            log.Error("Something has gone wrong", new Exception());
            log.Fatal("Oh dear, this is terrible");

            
            return View();
        }
    }
}
