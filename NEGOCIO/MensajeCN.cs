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
    public class MensajeCN : Singleton<MensajeCN>
    {

        public List<Mensaje> listar(String TICK_CODIGO, Int32 MENS_ESTADO)
        {
            try
            {
                return MensajeDAL.Instancia.listar(TICK_CODIGO, MENS_ESTADO);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
