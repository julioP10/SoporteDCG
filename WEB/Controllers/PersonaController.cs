using ENTIDAD;
using NEGOCIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WEB.Models;

namespace WEB.Controllers
{
    //[Authorize(Roles = "Administrador")]
    public class PersonaController : Controller{
        public JsonResult listar(int o, int l, string nombre = "", string id = "0")
        {
            try
            {
                var Persona = PersonaCN.Instancia.listar(nombre, id);
                var model = Persona.Skip(o).Take(l).ToList();
                var lista = from pe in model
                            select new
                            {
                                id = pe.PERSI_CODIGO,
                                nombre = pe.PERSV_NOMBRE,
                                paterno = pe.PERSV_APELLIDOS_PATERNO,
                                materno=pe.PERSV_APELLIDOS_MATERNO,
                                email = pe.PERSV_EMAIL,
                                direccion = pe.PERSV_DIRECCION,
                                fiscal = pe.PEDEV_DIRECCION_FISCAL,
                                razon = pe.PEDEV_RAZON_SOCIAL,
                                ruc = pe.PEDEC_RUC,
                                dni = pe.PEDEC_DNI,
                                fecha = pe.PERSD_FECHA_REG.ToString("dd/MM/yyyy"),
                                password = pe.PERSV_PASSWORD,
                                telefono = pe.PERSV_TELEFONO,
                                fechanac = pe.PERSD_FECHA_NAC.ToString("dd/MM/yyyy"),
                                img=pe.PERSV_IMG,
                                roleid=pe.RoleId,
                                rol=pe.RoleName
                            };
                return Json(new { count = Persona.Count, data = lista }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message });
            }
        }

        public JsonResult listarPersona()
        {
            try
            {
                var Persona = PersonaCN.Instancia.listar("", "").Where(p=>p.RoleName=="Cliente").ToList();
                return Json(new { count = Persona.Count, data = Persona}, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult> Registrar(FormCollection collection)
        {
            try
            {
                String mensaje = "", status = "SUCCESS";
                Persona persona = new Persona();
                //GENERAR CONTRASEÑA
                Random rnd = new Random();
                int contraseña = rnd.Next(100, 999);
                persona.PERSV_PASSWORD = ("C" + contraseña).ToString().Trim();


                persona.PERSI_CODIGO = collection["id"] == null ? "" : (collection["id"]);
                persona.PERSV_NOMBRE = collection["nombre"];
                persona.PERSV_APELLIDOS_PATERNO = collection["paterno"]==null? "": collection["paterno"];
                persona.PERSV_PASSWORD = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 7);
                persona.UserName = collection["usuario"] ==null? "": collection["usuario"];
                persona.PERSV_NOMBRE = persona.UserName;
                persona.RoleName = "Cliente";
                persona.PERSD_FECHA_NAC = Convert.ToDateTime("02/01/2007");

                if (persona.PERSI_CODIGO == "")
                {
                    AccountController ac = new AccountController();
                    var user = new ApplicationUser() { UserName = persona.UserName };
                    var result = await ac.UserManager.CreateAsync(user, persona.PERSV_PASSWORD);
                    persona.ID = user.Id;
                    if (result.Succeeded)
                    {
                        string host = HttpContext.Request.Url.Host;
                        if (host == "localhost")
                        {
                            host = host + ":" + HttpContext.Request.Url.Port;
                        }
                        await ac.UserManager.AddToRoleAsync(user.Id, persona.RoleName);
                        PersonaCN.Instancia.Registrar(persona);
                        mensaje = "SE REGISTRO CORRECTAMENTE Y SU CONTRASEÑA ES: " + persona.PERSV_PASSWORD;
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            mensaje = error;
                        }
                        status = "ERROR";
                    }
                }
                else
                {
                    //mensaje = "El usuario " + usuario + " ha sido actualizado exitosamente.";
                    //PersonaCN.Instancia.Registrar(persona);
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