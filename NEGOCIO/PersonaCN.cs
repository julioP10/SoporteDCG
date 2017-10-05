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
    public class PersonaCN : Singleton<PersonaCN>
    {

        public List<Persona> listar(string nombre, string ROLEID)
        {
            try
            {
                return PersonaDAL.Instancia.listar(nombre, ROLEID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Persona> listar_proveedor(Int32 persi_codigo, string nombre)
        {
            try
            {
                return PersonaDAL.Instancia.listar_proveedor(persi_codigo, nombre);
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
                return PersonaDAL.Instancia.listar_cliente(persi_codigo, nombre);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public virtual int Registrar(Persona Persona)
        {
            try
            {
                return PersonaDAL.Instancia.Registrar(Persona);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public virtual int Update(Persona Persona)
        {
            try
            {
                return PersonaDAL.Instancia.Update(Persona);
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
                return PersonaDAL.Instancia.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
