using ENTIDAD;
using ENTIDAD.GENERICS;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATOS
{
    public class UsuarioDAL : Singleton<UsuarioDAL>
    {
        private Database db = DatabaseFactory.CreateDatabase();
        public Persona obtener_por_id(string userId)
        {
            try
            {
                var coleccion = new Persona();
                DbCommand SQL = db.GetStoredProcCommand("USP_USUARIO_OBTENER_POR_USERID");
                db.AddInParameter(SQL, "PV_USERID", DbType.String, userId);
                using (var lector = db.ExecuteReader(SQL))
                {
                    while (lector.Read())
                    {
                        coleccion.PERSI_CODIGO = lector.IsDBNull(lector.GetOrdinal("PERSI_CODIGO")) ? "" : lector.GetString(lector.GetOrdinal("PERSI_CODIGO"));
                        coleccion.PERSV_NOMBRE = lector.IsDBNull(lector.GetOrdinal("PERSV_NOMBRE")) ? "" : lector.GetString(lector.GetOrdinal("PERSV_NOMBRE"));
                        coleccion.PERSV_APELLIDOS_PATERNO = lector.IsDBNull(lector.GetOrdinal("PERSV_APELLIDOS_PATERNO")) ? "" : lector.GetString(lector.GetOrdinal("PERSV_APELLIDOS_PATERNO"));
                        coleccion.PERSV_APELLIDOS_MATERNO = lector.IsDBNull(lector.GetOrdinal("PERSV_APELLIDOS_MATERNO")) ? "" : lector.GetString(lector.GetOrdinal("PERSV_APELLIDOS_MATERNO"));
                        coleccion.PERSV_DIRECCION = lector.IsDBNull(lector.GetOrdinal("PERSV_DIRECCION")) ? "" : lector.GetString(lector.GetOrdinal("PERSV_DIRECCION"));
                        coleccion.PERSV_TELEFONO = lector.IsDBNull(lector.GetOrdinal("PERSV_TELEFONO")) ? "" : lector.GetString(lector.GetOrdinal("PERSV_TELEFONO"));
                        coleccion.PERSV_EMAIL = lector.IsDBNull(lector.GetOrdinal("PERSV_EMAIL")) ? "" : lector.GetString(lector.GetOrdinal("PERSV_EMAIL"));
                        coleccion.ID = userId;
                        coleccion.RoleName = lector.IsDBNull(lector.GetOrdinal("ROLENAME")) ? "" : lector.GetString(lector.GetOrdinal("ROLENAME"));
                        coleccion.PERSV_IMG = lector.IsDBNull(lector.GetOrdinal("PERSV_IMG")) ? "" : lector.GetString(lector.GetOrdinal("PERSV_IMG"));
                        coleccion.PERSD_FECHA_NAC = lector.IsDBNull(lector.GetOrdinal("PERSD_FECHA_NAC")) ? default(DateTime) : lector.GetDateTime(lector.GetOrdinal("PERSD_FECHA_NAC"));
                    }
                }
                SQL.Dispose();
                return coleccion;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



    }
}
