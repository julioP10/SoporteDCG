using ENTIDAD;
using ENTIDAD.GENERICS;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATOS
{
    public class TipoTransaccionDAL : Singleton<TipoTransaccionDAL>
    {
        private Database db = DatabaseFactory.CreateDatabase();
        public List<TipoTransaccion> listar()
        {
            try
            {
                var coleccion = new List<TipoTransaccion>();
                DbCommand SQL = db.GetStoredProcCommand("USP_TRANSACCION_LISTAR");
                using (var lector = db.ExecuteReader(SQL))
                {
                    while (lector.Read())
                    {
                        coleccion.Add(new TipoTransaccion
                        {
                            TRANI_CODIGO = lector.IsDBNull(lector.GetOrdinal("TRANI_CODIGO")) ? default(Int32) : lector.GetInt32(lector.GetOrdinal("TRANI_CODIGO")),
                            TRANV_TRANSACCION = lector.IsDBNull(lector.GetOrdinal("TRANV_TRANSACCION")) ? default(string) : lector.GetString(lector.GetOrdinal("TRANV_TRANSACCION")),
                        });
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
