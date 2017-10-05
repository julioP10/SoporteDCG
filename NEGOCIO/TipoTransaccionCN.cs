using DATOS;
using ENTIDAD;
using ENTIDAD.GENERICS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEGOCIO
{
    public class TipoTransaccionCN : Singleton<TipoTransaccionCN>
    {
       public List<TipoTransaccion> listar()
        {
            try
            {
                return TipoTransaccionDAL.Instancia.listar();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
