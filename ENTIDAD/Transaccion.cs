using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDAD
{
   public class Transaccion
    {
       public int PROVI_CODIGO { get; set; }
       public int DOCUI_CODIGO { get; set; }
       public int TIOPI_CODIGO { get; set; }
       public int MONEI_CODIGO { get; set; }
       public int DOESI_CODIGO { get; set; }
       public int ALMAI_CODIGO { get; set; }
       public string CABEV_NUMERO_CODUCUMENTO { get; set; }
       public string CABEV_SERIE_DOCUMENTO { get; set; }
       public decimal CABED_IMPORTE { get; set; }
       public string XML_DETALLE{ get; set; }
       public Int32 TIOPI_TRANSACCION { get; set; }
       public Int32 CLIEI_CODIGO { get; set; }
    }
}
