using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ENTIDAD;
using NEGOCIO;

namespace WEB.Controllers
{
    public class TicketController : Controller
    {
        // GET: Ticket
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult listar(string codigo)
        {
           
            try
            {
            
                var Ticket = TicketCN.Instancia.listar("", codigo);
              
                
                return Json(new { count = Ticket.Count, data = Ticket }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult Registrar  (HttpPostedFileBase documento,FormCollection collection)
        {
            try
            {
                String mensaje = "", status = "SUCCESS";
                Ticket ticket = new Ticket();
                string usuario = collection["usuario"];
                var lista_persona = PersonaCN.Instancia.listar("","").Where(p=>p.PERSV_NOMBRE== usuario);
                foreach (var item in lista_persona) {
                    if (item.RoleName=="Cliente") {
                        ticket.PERSI_CODIGO = item.PERSI_CODIGO;
                        usuario = item.PERSV_NOMBRE;
                    }
                }
                ticket.TICK_CODIGO  = collection["id"] == null ? "" : (collection["id"]);
                ticket.TICK_CORREO = collection["correo"];
                ticket.TICK_RAZON_SOCIAL = collection["razon"];
                ticket.TICK_RUC = collection["ruc"];
                ticket.TICK_ASUNTO = collection["asunto"];
                ticket.PERF_CODIGO = collection["rol"];
                ticket.TICK_DESCRIPCION = collection["mensaje"];

                if (documento != null)
                {
                    string adjunto = DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(documento.FileName);
                    documento.SaveAs(Server.MapPath("~/Ticket/" + adjunto));
                    ticket.DOCA_DESCRIPCION = "~/Ticket/" + adjunto;


                }
                if (ticket.TICK_CODIGO=="") {
                    TicketCN.Instancia.Registrar(ticket);
                    mensaje = "se genero correctamente su ticket "+ usuario;
                }
                else {
                    TicketCN.Instancia.UPDATE(ticket);
                    mensaje = "se actualizo correctamente su ticket " + usuario;
                }

                return Json(new { status = status, message = mensaje }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = "ERROR", message = ex.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }
    }
   
}