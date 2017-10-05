using ENTIDAD;
using ENTIDAD.GENERICS;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace DATOS
{
    public class MensajeDAL : Singleton<MensajeDAL>
    {
        private Database db = DatabaseFactory.CreateDatabase();
        public List<Mensaje> listar(String TICK_CODIGO, Int32 MENS_ESTADO)
        {
            try
            {
                var coleccion = new List<Mensaje>();
                DbCommand SQL = db.GetStoredProcCommand("USP_MENSAJE_LISTA_CHAT");

                db.AddInParameter(SQL, "TICK_CODIGO", DbType.String, TICK_CODIGO);
                db.AddInParameter(SQL, "MENS_ESTADO", DbType.Int32, MENS_ESTADO);
                using (var dcg = db.ExecuteReader(SQL))
                {
                    while (dcg.Read())
                    {
                        //								

                        coleccion.Add(new Mensaje
                        {
                            PERSI_CODIGO = dcg.IsDBNull(dcg.GetOrdinal("PERSI_CODIGO")) ? default(string) : dcg.GetString(dcg.GetOrdinal("PERSI_CODIGO")),
                            TICK_CODIGO = dcg.IsDBNull(dcg.GetOrdinal("TICK_CODIGO")) ? default(string) : dcg.GetString(dcg.GetOrdinal("TICK_CODIGO")),
                            MENS_CODIGO = dcg.IsDBNull(dcg.GetOrdinal("MENS_CODIGO")) ? default(string) : dcg.GetString(dcg.GetOrdinal("MENS_CODIGO")),
                            PERSV_NOMBRE = dcg.IsDBNull(dcg.GetOrdinal("PERSV_NOMBRE")) ? default(string) : dcg.GetString(dcg.GetOrdinal("PERSV_NOMBRE")),
                            MENS_MENSAJE = dcg.IsDBNull(dcg.GetOrdinal("MENS_MENSAJE")) ? default(string) : dcg.GetString(dcg.GetOrdinal("MENS_MENSAJE")),
                            MENS_VIGENCIA = dcg.IsDBNull(dcg.GetOrdinal("MENS_VIGENCIA")) ? 0 : dcg.GetInt32(dcg.GetOrdinal("MENS_VIGENCIA")),
                            MENS_ESTADO = dcg.IsDBNull(dcg.GetOrdinal("MENS_ESTADO")) ? 0 : dcg.GetInt32(dcg.GetOrdinal("MENS_ESTADO")),
                            Name = dcg.IsDBNull(dcg.GetOrdinal("Name")) ? default(string) : dcg.GetString(dcg.GetOrdinal("Name")),
                            //TICK_VIGENCIA = dcg.IsDBNull(dcg.GetOrdinal("TICK_VIGENCIA")) ? default(int) : dcg.GetInt32(dcg.GetOrdinal("TICK_VIGENCIA")),
                            MENS_FECHA = dcg.IsDBNull(dcg.GetOrdinal("MENS_FECHA")) ? default(DateTime) : dcg.GetDateTime(dcg.GetOrdinal("MENS_FECHA")),

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