using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using NEGOCIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEB.Models;

namespace WEB.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class UsuarioController : Controller
    {

        ApplicationDbContext context;
        public UserManager<ApplicationUser> UserManager { get; private set; }
        public UsuarioController()
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
            context = new ApplicationDbContext();       // context inicializado en constructor
        }
        private UsuarioController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }


        [NonAction]
        public static UsuarioModel ObtenerUsuarioActual(HttpContextBase context)
        {
            UsuarioModel usuario;
            if (context.Session["usuario"] == null && context.User.Identity.Name != "")
            {
                var persona = UsuarioCN.Instancia.obtener_por_id(context.User.Identity.GetUserId());
                usuario = new UsuarioModel(persona);
                context.Session["usuario"] = usuario;
            }
            else
            {
                usuario = (UsuarioModel)context.Session["usuario"];
            }
            return usuario;
        }


	}
}