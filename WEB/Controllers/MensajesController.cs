using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ENTIDAD;
using NEGOCIO;

namespace WEB.Controllers
{
    public class MensajesController : Controller
    {
        // GET: Mensajes
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult ListarMensajes(string codigo="",int estado=0) {
            try
            {
        
                var Mensajes = MensajeCN.Instancia.listar(codigo, estado);

                return Json(new { count = Mensajes.Count, data = Mensajes }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message });
            }
        }
    }
}