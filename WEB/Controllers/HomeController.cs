using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WEB.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

         
    }
}