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
    public class PersonaTipoDAL:Singleton<PersonaTipoDAL>
    {
        private Database db = DatabaseFactory.CreateDatabase();
        public List<PersonaTipo> listar()
        {
            try
            {
                var coleccion = new List<PersonaTipo>();
                DbCommand SQL = db.GetStoredProcCommand("USP_TIPO_PERSONA_LISTAR");
                using (var lector = db.ExecuteReader(SQL))
                {
                    while (lector.Read())
                    {
                        coleccion.Add(new PersonaTipo
                        {
                            TIPPI_CODIGO = lector.IsDBNull(lector.GetOrdinal("TIPPI_CODIGO")) ? default(Int32) : lector.GetInt32(lector.GetOrdinal("TIPPI_CODIGO")),
                            TIPPV_TIPO_PERSONA = lector.IsDBNull(lector.GetOrdinal("TIPPV_TIPO_PERSONA")) ? default(string) : lector.GetString(lector.GetOrdinal("TIPPV_TIPO_PERSONA")),
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
