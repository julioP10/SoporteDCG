using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDAD
{
   public class Ticket
    {
        public string TICK_CODIGO { get; set; }
        public string PERSI_CODIGO { get; set; }
        
        public string TICK_CORREO { get; set; }
        public string TICK_RAZON_SOCIAL { get; set; }
        public string TICK_RUC { get; set; }
        public string TICK_ASUNTO { get; set; }
        public string PERF_CODIGO { get; set; }
        public string TICK_DESCRIPCION { get; set; }
        public DateTime TICK_FECHA { get; set; }
        public int TICK_VIGENCIA { get; set; }
        public string DOCA_DESCRIPCION { get; set; }
    }
}
