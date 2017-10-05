using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.Mvc;
using WEB.Models;
using System.Linq;

namespace WEB.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class AdministradorController : Controller
    {

        #region context
            ApplicationDbContext context;
            public UserManager<ApplicationUser> UserManager { get; private set; }
            public AdministradorController()
                : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
            {
                context = new ApplicationDbContext();       // context inicializado en constructor
            }
            private AdministradorController(UserManager<ApplicationUser> userManager)
            {
                UserManager = userManager;
            }
        #endregion
        //
        // GET: /Administrador/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Mensaje()
        {
            return View("../Administrador/Mensajes/Index");
        }
        public ActionResult Modelo()
        {
            return View("../Administrador/Modelo/Index");
        }
        public ActionResult Linea()
        {
            return View("../Administrador/Linea/Index");
        }
        public ActionResult Producto()
        {
            return View("../Administrador/Producto/Index");
        }
        public ActionResult Persona()
        {
            var Roles = context.Roles.ToList();
            return View("../Administrador/Persona/Index", Roles);
        }
        public ActionResult Compra()
        {
            return View("../Administrador/Compra/Index");
        }
        public ActionResult Venta()
        {
            return View("../Administrador/Venta/Index");
        }


        public ActionResult Ticket() {
            return View("../Administrador/Ticket/Index");
        }

        // reportes
        public ActionResult REP_Producto()
        {
            return View("../Administrador/Reporte/Producto/Index");
        }
        public ActionResult REP_venta()
        {
            return View("../Administrador/Reporte/Venta/Index");
        }
        public ActionResult REP_Compra()
        {
            return View("../Administrador/Reporte/Compra/Index");
        }

	}
}