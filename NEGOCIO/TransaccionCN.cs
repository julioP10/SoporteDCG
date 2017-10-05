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
    public class TransaccionCN : Singleton<TransaccionCN>
    {
        public int INSERT_COMPRA(Transaccion transacion)
        {
            try
            {
                return TransaccionDAL.Instancia.INSERT_COMPRA(transacion);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }  
        public int INSERT_VENTA(Transaccion transacion)
        {
            try
            {
                return TransaccionDAL.Instancia.INSERT_VENTA(transacion);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }  
               
    }
}
