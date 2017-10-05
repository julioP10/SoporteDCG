using ENTIDAD;
using ENTIDAD.GENERICS;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace DATOS
{
   public class TicketDAL : Singleton<TicketDAL>
    { 
    private Database db = DatabaseFactory.CreateDatabase();
    public List<Ticket> listar(String TICK_CORREO, String TICK_CODIGO)
    {
        try
        {
            var coleccion = new List<Ticket>();
            DbCommand SQL = db.GetStoredProcCommand("USP_Ticket_LISTAR");

            db.AddInParameter(SQL, "TICK_CORREO", DbType.String, TICK_CORREO);
                db.AddInParameter(SQL, "TICK_CODIGO", DbType.String, TICK_CODIGO);
                using (var lector = db.ExecuteReader(SQL))
            {
                while (lector.Read())
                {

                 coleccion.Add(new Ticket
                    {
                        TICK_CODIGO = lector.IsDBNull(lector.GetOrdinal("TICK_CODIGO")) ? default(string) : lector.GetString(lector.GetOrdinal("TICK_CODIGO")),
                        TICK_CORREO = lector.IsDBNull(lector.GetOrdinal("TICK_CORREO")) ? default(string) : lector.GetString(lector.GetOrdinal("TICK_CORREO")),
                        TICK_RAZON_SOCIAL = lector.IsDBNull(lector.GetOrdinal("TICK_RAZON_SOCIAL")) ? default(string) : lector.GetString(lector.GetOrdinal("TICK_RAZON_SOCIAL")),
                        TICK_RUC = lector.IsDBNull(lector.GetOrdinal("TICK_RUC")) ? default(string) : lector.GetString(lector.GetOrdinal("TICK_RUC")),
                        TICK_ASUNTO = lector.IsDBNull(lector.GetOrdinal("TICK_ASUNTO")) ? default(string) : lector.GetString(lector.GetOrdinal("TICK_ASUNTO")),
                        PERF_CODIGO = lector.IsDBNull(lector.GetOrdinal("PERF_CODIGO")) ? default(string) : lector.GetString(lector.GetOrdinal("PERF_CODIGO")),
                        TICK_DESCRIPCION = lector.IsDBNull(lector.GetOrdinal("TICK_DESCRIPCION")) ? default(string) : lector.GetString(lector.GetOrdinal("TICK_DESCRIPCION")),
                        PERSI_CODIGO = lector.IsDBNull(lector.GetOrdinal("PERSI_CODIGO")) ? default(string) : lector.GetString(lector.GetOrdinal("PERSI_CODIGO")),
                     //   DOCA_DESCRIPCION = lector.IsDBNull(lector.GetOrdinal("DOCA_DESCRIPCION")) ? default(string) : lector.GetString(lector.GetOrdinal("DOCA_DESCRIPCION")),
                     ////TICK_VIGENCIA = lector.IsDBNull(lector.GetOrdinal("TICK_VIGENCIA")) ? default(int) : lector.GetInt32(lector.GetOrdinal("TICK_VIGENCIA")),
                     TICK_FECHA = lector.IsDBNull(lector.GetOrdinal("TICK_FECHA")) ? default(DateTime) : lector.GetDateTime(lector.GetOrdinal("TICK_FECHA")),

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
        public int Registrar(Ticket Ticket, DbTransaction tran = null)
        {
            try
            {
                if (Ticket.TICK_CODIGO == "")
                    Ticket.TICK_CODIGO = INSERT(Ticket, tran).ToString();
                else UPDATE(Ticket, tran);

                return Int32.Parse(Ticket.TICK_CODIGO);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int INSERT(Ticket ticket, DbTransaction tran = null)
    {
        try
        {


            DbCommand SQL = db.GetStoredProcCommand("USP_TICKET_INSERT");
            db.AddInParameter(SQL, "TICK_CORREO", DbType.String, ticket.TICK_CORREO);
            db.AddInParameter(SQL, "TICK_RAZON_SOCIAL", DbType.String, ticket.TICK_RAZON_SOCIAL);
            db.AddInParameter(SQL, "TICK_RUC", DbType.String, ticket.TICK_RUC);
            db.AddInParameter(SQL, "TICK_ASUNTO", DbType.String, ticket.TICK_ASUNTO);
            db.AddInParameter(SQL, "PERF_CODIGO", DbType.String, ticket.PERF_CODIGO);
            db.AddInParameter(SQL, "TICK_DESCRIPCION", DbType.String, ticket.TICK_DESCRIPCION);
            db.AddInParameter(SQL, "PERSI_CODIGO", DbType.String, ticket.PERSI_CODIGO);
            db.AddInParameter(SQL, "DOCA_DESCRIPCION", DbType.String, ticket.DOCA_DESCRIPCION);
                int huboexito = 0;
                if (tran == null) huboexito = db.ExecuteNonQuery(SQL);
                else huboexito = db.ExecuteNonQuery(SQL, tran);

                if (huboexito == 0)
                {
                    throw new Exception("Error al agregar al");
                }
                SQL.Dispose();
     
                return huboexito;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public int UPDATE(Ticket ticket, DbTransaction tran = null)
    {
        try
        {
            DbCommand SQL = db.GetStoredProcCommand("USP_Ticket_UPDATE");
            db.AddInParameter(SQL, "TICK_CODIGO", DbType.String, ticket.TICK_CODIGO);
            db.AddInParameter(SQL, "TICK_CORREO", DbType.String, ticket.TICK_CORREO);
            db.AddInParameter(SQL, "TICK_RAZON_SOCIAL", DbType.String, ticket.TICK_RAZON_SOCIAL);
            db.AddInParameter(SQL, "TICK_RUC", DbType.String, ticket.TICK_RUC);
            db.AddInParameter(SQL, "TICK_ASUNTO", DbType.String, ticket.TICK_ASUNTO);
            db.AddInParameter(SQL, "PERF_CODIGO", DbType.String, ticket.PERF_CODIGO);
            db.AddInParameter(SQL, "TICK_DESCRIPCION", DbType.String, ticket.TICK_DESCRIPCION);
            db.AddInParameter(SQL, "DOCA_DESCRIPCION", DbType.String, ticket.DOCA_DESCRIPCION);
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
    public virtual Int32 DELETE(Int32 id)
    {
        try
        {
            DbCommand SQL = db.GetStoredProcCommand("USP_Ticket_DELETE");
            db.AddInParameter(SQL, "TICK_CODIGO", DbType.Int32, id);
            int huboexito = db.ExecuteNonQuery(SQL);
            if (huboexito == 0)
            {
                throw new Exception("Error al agregar al");
            }
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
