using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDAD
{
    public class Persona
    {
        public string PERSI_CODIGO { get; set; }
        public string PERSV_PASSWORD { get; set; }
        public string PERSV_NOMBRE { get; set; }
        public string PERSV_APELLIDOS_PATERNO { get; set; }
        public string PERSV_APELLIDOS_MATERNO { get; set; }
        public string PERSV_DIRECCION { get; set; }
        public string PERSV_EMAIL { get; set; }
        public string PERSV_TELEFONO { get; set; }
        public DateTime PERSD_FECHA_NAC { get; set; }
        public string PERSV_IMG { get; set; }
        public string ID { get; set; }
        public string PEDEC_DNI { get; set; }
        public string PEDEV_RAZON_SOCIAL { get; set; }
        public string PEDEC_RUC { get; set; }
        public string PEDEV_DIRECCION_FISCAL { get; set; }
        public string RoleName { get; set; }
        public string UserName { get; set; }
        public DateTime PERSD_FECHA_REG { get; set; }
        public string RoleId { get; set; }
        public int PROVI_CODIGO { get; set; }
        public decimal PROVD_DEUDA { get; set; }
        public decimal CLIED_DEUDA { get; set; }
        public int CLIEI_CODIGO { get; set; }
    }
}
