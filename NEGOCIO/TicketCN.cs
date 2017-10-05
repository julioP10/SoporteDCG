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
   public class TicketCN : Singleton<TicketCN>
    {

        public List<Ticket> listar(String TICK_CORREO, String TICK_CODIGO)
        {
            try
            {
                return TicketDAL.Instancia.listar(TICK_CORREO, TICK_CODIGO);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int Registrar(Ticket ticket)
        {
            try
            {
                return TicketDAL.Instancia.Registrar(ticket);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int UPDATE(Ticket ticket)
        {
            try
            {
                return TicketDAL.Instancia.UPDATE(ticket);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public virtual Int32 Delete(Int32 id)
        {
            try
            {
                return TicketDAL.Instancia.DELETE(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}