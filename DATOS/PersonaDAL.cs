using ENTIDAD;
using ENTIDAD.GENERICS;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace DATOS
{
    public class PersonaDAL : Singleton<PersonaDAL>
    { // crea los procedimientos y haz igual para todas las tablas editar eliminar actulizar y listar en degovio es asi
        private Database db = DatabaseFactory.CreateDatabase();
        public List<Persona> listar(String NOMBRE, string ROLEID)
        {
            try
            {
                var coleccion = new List<Persona>();
                DbCommand SQL = db.GetStoredProcCommand("USP_PERSONA_LISTAR");
                db.AddInParameter(SQL, "PV_PERSV_NOMBRE", DbType.String, NOMBRE);
                db.AddInParameter(SQL, "PV_ROLE_ID", DbType.String, ROLEID);
                using (var dcg = db.ExecuteReader(SQL))
                {
                    while (dcg.Read())
                    {
                        coleccion.Add(new Persona
                        {

                            PERSI_CODIGO = dcg.IsDBNull(dcg.GetOrdinal("PERSI_CODIGO")) ? "" : dcg.GetString(dcg.GetOrdinal("PERSI_CODIGO")),
                            PERSV_NOMBRE = dcg.IsDBNull(dcg.GetOrdinal("PERSV_NOMBRE")) ? default(string) : dcg.GetString(dcg.GetOrdinal("PERSV_NOMBRE")),
                            PERSV_PASSWORD = dcg.IsDBNull(dcg.GetOrdinal("PERSV_PASSWORD")) ? default(string) : dcg.GetString(dcg.GetOrdinal("PERSV_PASSWORD")),
                            RoleName = dcg.IsDBNull(dcg.GetOrdinal("name")) ? default(string) : dcg.GetString(dcg.GetOrdinal("name")),
                            PERSV_APELLIDOS_PATERNO = dcg.IsDBNull(dcg.GetOrdinal("PERSV_APELLIDOS_PATERNO")) ? default(string) : dcg.GetString(dcg.GetOrdinal("PERSV_APELLIDOS_PATERNO")),
                            PERSV_APELLIDOS_MATERNO = dcg.IsDBNull(dcg.GetOrdinal("PERSV_APELLIDOS_MATERNO")) ? default(string) : dcg.GetString(dcg.GetOrdinal("PERSV_APELLIDOS_MATERNO")),
                            PERSV_DIRECCION = dcg.IsDBNull(dcg.GetOrdinal("PERSV_DIRECCION")) ? default(string) : dcg.GetString(dcg.GetOrdinal("PERSV_DIRECCION")),
                            PERSV_TELEFONO = dcg.IsDBNull(dcg.GetOrdinal("PERSV_TELEFONO")) ? default(string) : dcg.GetString(dcg.GetOrdinal("PERSV_TELEFONO")),
                            PERSD_FECHA_NAC = dcg.IsDBNull(dcg.GetOrdinal("PERSD_FECHA_NAC")) ? default(DateTime) : dcg.GetDateTime(dcg.GetOrdinal("PERSD_FECHA_NAC")),
                            PERSV_EMAIL = dcg.IsDBNull(dcg.GetOrdinal("PERSV_EMAIL")) ? "" : dcg.GetString(dcg.GetOrdinal("PERSV_EMAIL")),
                            PERSV_IMG = dcg.IsDBNull(dcg.GetOrdinal("PERSV_IMG")) ? default(string) : dcg.GetString(dcg.GetOrdinal("PERSV_IMG")),
                            PEDEC_DNI = dcg.IsDBNull(dcg.GetOrdinal("PEDEC_DNI")) ? default(string) : dcg.GetString(dcg.GetOrdinal("PEDEC_DNI")),
                            PEDEV_RAZON_SOCIAL = dcg.IsDBNull(dcg.GetOrdinal("PEDEV_RAZON_SOCIAL")) ? default(string) : dcg.GetString(dcg.GetOrdinal("PEDEV_RAZON_SOCIAL")),
                            PEDEC_RUC = dcg.IsDBNull(dcg.GetOrdinal("PEDEC_RUC")) ? default(string) : dcg.GetString(dcg.GetOrdinal("PEDEC_RUC")),
                            PEDEV_DIRECCION_FISCAL = dcg.IsDBNull(dcg.GetOrdinal("PEDEV_DIRECCION_FISCAL")) ? default(string) : dcg.GetString(dcg.GetOrdinal("PEDEV_DIRECCION_FISCAL")),
                            PERSD_FECHA_REG = dcg.IsDBNull(dcg.GetOrdinal("PERSD_FECHA_REG")) ? default(DateTime) : dcg.GetDateTime(dcg.GetOrdinal("PERSD_FECHA_REG")),
                            RoleId = dcg.IsDBNull(dcg.GetOrdinal("RoleId")) ? default(string) : dcg.GetString(dcg.GetOrdinal("RoleId")),
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
        public int Registrar(Persona Persona, DbTransaction tran = null)
        {
            try
            {
                if (Persona.PERSI_CODIGO == "") 
                    Persona.PERSI_CODIGO = Insert(Persona, tran).ToString();
                else Update(Persona, tran);

                return Int32.Parse(Persona.PERSI_CODIGO);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private int Insert(Persona Persona, DbTransaction tran = null)
        {
            try
            {
                DbCommand SQL = db.GetStoredProcCommand("USP_PERSONA_INSERT");
                db.AddInParameter(SQL, "PV_PERSV_NOMBRE", DbType.String, Persona.PERSV_NOMBRE);
                db.AddInParameter(SQL, "PV_PERSV_APELLIDOS_PATERNO", DbType.String, Persona.PERSV_APELLIDOS_PATERNO);
                db.AddInParameter(SQL, "PV_PERSV_APELLIDOS_MATERNO", DbType.String, Persona.PERSV_APELLIDOS_MATERNO);
                db.AddInParameter(SQL, "PV_PERSV_DIRECCION", DbType.String, Persona.PERSV_DIRECCION);
                db.AddInParameter(SQL, "PV_PERSV_PASSWORD", DbType.String, Persona.PERSV_PASSWORD);
                db.AddInParameter(SQL, "PC_PERSV_TELEFONO", DbType.String, Persona.PERSV_TELEFONO);
                db.AddInParameter(SQL, "PD_PERSD_FECHA_NAC", DbType.DateTime, Persona.PERSD_FECHA_NAC);
                db.AddInParameter(SQL, "PN_ID", DbType.String, Persona.ID);
                db.AddInParameter(SQL, "PV_PERSV_EMAIL", DbType.String, Persona.PERSV_EMAIL);
                db.AddInParameter(SQL, "PC_PEDEC_DNI", DbType.String, Persona.PEDEC_DNI);
                db.AddInParameter(SQL, "PV_PEDEV_RAZON_SOCIAL", DbType.String, Persona.PEDEV_RAZON_SOCIAL);
                db.AddInParameter(SQL, "PC_PEDEC_RUC", DbType.String, Persona.PEDEC_RUC);
                db.AddInParameter(SQL, "PV_PERSV_IMG", DbType.String, Persona.PERSV_IMG);
                db.AddInParameter(SQL, "PV_PEDEV_DIRECCION_FISCAL", DbType.String, Persona.PEDEV_DIRECCION_FISCAL);
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
        public virtual int Update(Persona Persona, DbTransaction tran = null)
        {
            try
            {
                DbCommand SQL = db.GetStoredProcCommand("USP_PERSONA_GENERAL_UPDATE");
                db.AddInParameter(SQL, "PI_PERSI_CODIGO", DbType.Int32, Persona.PERSI_CODIGO);
                db.AddInParameter(SQL, "PV_PERSV_NOMBRE", DbType.String, Persona.PERSV_NOMBRE);
                db.AddInParameter(SQL, "PV_PERSV_APELLIDOS_PATERNO", DbType.String, Persona.PERSV_APELLIDOS_PATERNO);
                db.AddInParameter(SQL, "PV_PERSV_APELLIDOS_MATERNO", DbType.String, Persona.PERSV_APELLIDOS_MATERNO);
                db.AddInParameter(SQL, "PD_PERSD_FECHA_NAC", DbType.DateTime, Persona.PERSD_FECHA_NAC);
                db.AddInParameter(SQL, "PV_PERSV_DIRECCION", DbType.String, Persona.PERSV_DIRECCION);
                db.AddInParameter(SQL, "PC_PERSV_TELEFONO", DbType.String, Persona.PERSV_TELEFONO);
                db.AddInParameter(SQL, "PV_PERSV_EMAIL", DbType.String, Persona.PERSV_EMAIL);
                db.AddInParameter(SQL, "PC_PEDEC_DNI", DbType.String, Persona.PEDEC_DNI);
                db.AddInParameter(SQL, "PV_PEDEV_RAZON_SOCIAL", DbType.String, Persona.PEDEV_RAZON_SOCIAL);
                db.AddInParameter(SQL, "PC_PEDEC_RUC", DbType.String, Persona.PEDEC_RUC);
                db.AddInParameter(SQL, "PV_PERSV_IMG", DbType.String, Persona.PERSV_IMG);
                db.AddInParameter(SQL, "PV_PEDEV_DIRECCION_FISCAL", DbType.String, Persona.PEDEV_DIRECCION_FISCAL);
                db.AddInParameter(SQL, "PV_PERSV_PASSWORD", DbType.String, Persona.PERSV_PASSWORD);
                int huboexito;
                if (tran == null) huboexito = db.ExecuteNonQuery(SQL);
                else huboexito = db.ExecuteNonQuery(SQL, tran);

                if (huboexito == 0)
                {
                    throw new Exception("Error");
                }
                return huboexito;
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
                DbCommand SQL = db.GetStoredProcCommand("USP_PERSONA_GENERAL_DELETE");
                db.AddInParameter(SQL, "PI_PERSI_CODIGO", DbType.Int32, id);
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
        /*==========================*/
        public List<Persona> listar_proveedor(Int32 persi_codigo, string nombre)
        {
            try
            {
                var coleccion = new List<Persona>();
                DbCommand SQL = db.GetStoredProcCommand("USP_PROVEEDOR_LISTAR");
                db.AddInParameter(SQL, "PI_PERSI_CODIGO", DbType.Int32, persi_codigo);
                db.AddInParameter(SQL, "PV_PERSV_NOMBRE", DbType.String, nombre);
                using (var dcg = db.ExecuteReader(SQL))
                {
                    while (dcg.Read())
                    {
                        coleccion.Add(new Persona
                        {

                            PROVI_CODIGO = dcg.IsDBNull(dcg.GetOrdinal("PROVI_CODIGO")) ? default(Int32) : dcg.GetInt32(dcg.GetOrdinal("PROVI_CODIGO")),
                            PEDEV_RAZON_SOCIAL = dcg.IsDBNull(dcg.GetOrdinal("PEDEV_RAZON_SOCIAL")) ? default(string) : dcg.GetString(dcg.GetOrdinal("PEDEV_RAZON_SOCIAL")),
                            PROVD_DEUDA = dcg.IsDBNull(dcg.GetOrdinal("PROVD_DEUDA")) ? default(decimal) : dcg.GetDecimal(dcg.GetOrdinal("PROVD_DEUDA")),
                            PERSV_APELLIDOS_PATERNO = dcg.IsDBNull(dcg.GetOrdinal("PERSV_APELLIDOS_PATERNO")) ? default(string) : dcg.GetString(dcg.GetOrdinal("PERSV_APELLIDOS_PATERNO")),
                            PERSV_APELLIDOS_MATERNO = dcg.IsDBNull(dcg.GetOrdinal("PERSV_APELLIDOS_MATERNO")) ? default(string) : dcg.GetString(dcg.GetOrdinal("PERSV_APELLIDOS_MATERNO")),
                            PERSV_DIRECCION = dcg.IsDBNull(dcg.GetOrdinal("PERSV_DIRECCION")) ? default(string) : dcg.GetString(dcg.GetOrdinal("PERSV_DIRECCION")),
                            PERSV_TELEFONO = dcg.IsDBNull(dcg.GetOrdinal("PERSV_TELEFONO")) ? default(string) : dcg.GetString(dcg.GetOrdinal("PERSV_TELEFONO")),
                            PERSV_EMAIL = dcg.IsDBNull(dcg.GetOrdinal("PERSV_EMAIL")) ? "" : dcg.GetString(dcg.GetOrdinal("PERSV_EMAIL")),
                            PERSV_IMG = dcg.IsDBNull(dcg.GetOrdinal("PERSV_IMG")) ? default(string) : dcg.GetString(dcg.GetOrdinal("PERSV_IMG")),
                            PEDEC_DNI = dcg.IsDBNull(dcg.GetOrdinal("PEDEC_DNI")) ? default(string) : dcg.GetString(dcg.GetOrdinal("PEDEC_DNI")),
                            PEDEC_RUC = dcg.IsDBNull(dcg.GetOrdinal("PEDEC_RUC")) ? default(string) : dcg.GetString(dcg.GetOrdinal("PEDEC_RUC")),
                            PEDEV_DIRECCION_FISCAL = dcg.IsDBNull(dcg.GetOrdinal("PEDEV_DIRECCION_FISCAL")) ? default(string) : dcg.GetString(dcg.GetOrdinal("PEDEV_DIRECCION_FISCAL")),

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
        public List<Persona> listar_cliente(Int32 persi_codigo, string nombre)
        {
            try
            {
                var coleccion = new List<Persona>();
                DbCommand SQL = db.GetStoredProcCommand("USP_CLIENTE_LISTAR");
                db.AddInParameter(SQL, "PI_PERSI_CODIGO", DbType.Int32, persi_codigo);
                db.AddInParameter(SQL, "PV_PERSV_NOMBRE", DbType.String, nombre);
                using (var dcg = db.ExecuteReader(SQL))
                {
                    while (dcg.Read())
                    {
                        coleccion.Add(new Persona
                        {

                            CLIEI_CODIGO = dcg.IsDBNull(dcg.GetOrdinal("CLIEI_CODIGO")) ? default(Int32) : dcg.GetInt32(dcg.GetOrdinal("CLIEI_CODIGO")),
                            PEDEV_RAZON_SOCIAL = dcg.IsDBNull(dcg.GetOrdinal("PEDEV_RAZON_SOCIAL")) ? default(string) : dcg.GetString(dcg.GetOrdinal("PEDEV_RAZON_SOCIAL")),
                            CLIED_DEUDA = dcg.IsDBNull(dcg.GetOrdinal("CLIED_DEUDA")) ? default(decimal) : dcg.GetDecimal(dcg.GetOrdinal("CLIED_DEUDA")),
                            PERSV_APELLIDOS_PATERNO = dcg.IsDBNull(dcg.GetOrdinal("PERSV_APELLIDOS_PATERNO")) ? default(string) : dcg.GetString(dcg.GetOrdinal("PERSV_APELLIDOS_PATERNO")),
                            PERSV_APELLIDOS_MATERNO = dcg.IsDBNull(dcg.GetOrdinal("PERSV_APELLIDOS_MATERNO")) ? default(string) : dcg.GetString(dcg.GetOrdinal("PERSV_APELLIDOS_MATERNO")),
                            PERSV_DIRECCION = dcg.IsDBNull(dcg.GetOrdinal("PERSV_DIRECCION")) ? default(string) : dcg.GetString(dcg.GetOrdinal("PERSV_DIRECCION")),
                            PERSV_TELEFONO = dcg.IsDBNull(dcg.GetOrdinal("PERSV_TELEFONO")) ? default(string) : dcg.GetString(dcg.GetOrdinal("PERSV_TELEFONO")),
                            PERSV_EMAIL = dcg.IsDBNull(dcg.GetOrdinal("PERSV_EMAIL")) ? "" : dcg.GetString(dcg.GetOrdinal("PERSV_EMAIL")),
                            PERSV_IMG = dcg.IsDBNull(dcg.GetOrdinal("PERSV_IMG")) ? default(string) : dcg.GetString(dcg.GetOrdinal("PERSV_IMG")),
                            PEDEC_DNI = dcg.IsDBNull(dcg.GetOrdinal("PEDEC_DNI")) ? default(string) : dcg.GetString(dcg.GetOrdinal("PEDEC_DNI")),
                            PEDEC_RUC = dcg.IsDBNull(dcg.GetOrdinal("PEDEC_RUC")) ? default(string) : dcg.GetString(dcg.GetOrdinal("PEDEC_RUC")),
                            PEDEV_DIRECCION_FISCAL = dcg.IsDBNull(dcg.GetOrdinal("PEDEV_DIRECCION_FISCAL")) ? default(string) : dcg.GetString(dcg.GetOrdinal("PEDEV_DIRECCION_FISCAL")),

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


    }
}
