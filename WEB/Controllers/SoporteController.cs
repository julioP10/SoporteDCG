using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEB.Models;

namespace WEB.Controllers
{
    public class SoporteController : Controller
    {
        // GET: Soporte
        public ActionResult Index()
        {
            var context = new ApplicationDbContext();
            var roles = context.Roles.Where(p => p.Name != "Administrador" && p.Name != "Cliente").ToList();
            return View(roles);

        }
    }
}