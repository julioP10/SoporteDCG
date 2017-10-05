using ENTIDAD;
using ENTIDAD.GENERICS;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace DATOS
{
    public class TransaccionDAL : Singleton<TransaccionDAL>
    {
        private Database db = DatabaseFactory.CreateDatabase();
        public int INSERT_COMPRA(Transaccion transacion)
        {
            try
            {
                DbCommand SQL = db.GetStoredProcCommand("USP_COMPRA_INSERT");
                db.AddInParameter(SQL, "PI_PROVI_CODIGO", DbType.Int32, transacion.PROVI_CODIGO);
                db.AddInParameter(SQL, "PI_DOCUI_CODIGO", DbType.Int32, transacion.DOCUI_CODIGO);
                db.AddInParameter(SQL, "PI_TIOPI_CODIGO", DbType.Int32, transacion.TIOPI_CODIGO);
                db.AddInParameter(SQL, "PI_MONEI_CODIGO", DbType.Int32, transacion.MONEI_CODIGO);
                db.AddInParameter(SQL, "PI_DOESI_CODIGO", DbType.Int32, transacion.DOESI_CODIGO);
                db.AddInParameter(SQL, "PI_ALMAI_CODIGO", DbType.Int32, transacion.ALMAI_CODIGO);
                db.AddInParameter(SQL, "PV_CABEV_NUMERO_CODUCUMENTO", DbType.String, transacion.CABEV_NUMERO_CODUCUMENTO);
                db.AddInParameter(SQL, "PV_CABEV_SERIE_DOCUMENTO", DbType.String, transacion.CABEV_SERIE_DOCUMENTO);
                db.AddInParameter(SQL, "PV_CABED_IMPORTE", DbType.Decimal, transacion.CABED_IMPORTE);
                db.AddInParameter(SQL, "PX_XML_DETALLE", DbType.Xml, transacion.XML_DETALLE);
                int huboexito = 0;
                huboexito = db.ExecuteNonQuery(SQL);
                if (huboexito == 0)
                {
                    throw new Exception("Error al agregar al");
                }
                // var numerogenerado = (int)db.GetParameterValue(SQL, "PI_PERSI_CODIGO");
                SQL.Dispose();
                return huboexito;
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
                DbCommand SQL = db.GetStoredProcCommand("USP_VENTA_INSERT");
                db.AddInParameter(SQL, "PI_CLIEI_CODIGO", DbType.Int32, transacion.CLIEI_CODIGO);
                db.AddInParameter(SQL, "PI_DOCUI_CODIGO", DbType.Int32, transacion.DOCUI_CODIGO);
                db.AddInParameter(SQL, "PI_TIOPI_CODIGO", DbType.Int32, transacion.TIOPI_CODIGO);
                db.AddInParameter(SQL, "PI_MONEI_CODIGO", DbType.Int32, transacion.MONEI_CODIGO);
                db.AddInParameter(SQL, "PI_DOESI_CODIGO", DbType.Int32, transacion.DOESI_CODIGO);
                db.AddInParameter(SQL, "PI_TRANI_CODIGO", DbType.Int32, transacion.TIOPI_TRANSACCION);
                db.AddInParameter(SQL, "PV_VECAV_NUMERO_CODUCUMENTO", DbType.String, transacion.CABEV_NUMERO_CODUCUMENTO);
                db.AddInParameter(SQL, "PV_VECAV_SERIE_CODUCUMENTO", DbType.String, transacion.CABEV_SERIE_DOCUMENTO);
                db.AddInParameter(SQL, "PD_VECAD_IMPORTE", DbType.Decimal, transacion.CABED_IMPORTE);
                db.AddInParameter(SQL, "PX_XML_DETALLE", DbType.String, transacion.XML_DETALLE);
                int huboexito = 0;
                huboexito = db.ExecuteNonQuery(SQL);
                if (huboexito == 0)
                {
                    throw new Exception("Error al agregar al");
                }
                // var numerogenerado = (int)db.GetParameterValue(SQL, "PI_PERSI_CODIGO");
                SQL.Dispose();
                return huboexito;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

      
    }
}
