﻿using AnimalStore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnimalStore.Services.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            DataContext dataContext = new DataContext();
            dataContext.Database.Initialize(true);

            return View();
        }
    }
}
