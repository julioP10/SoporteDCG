using DATOS;
using ENTIDAD;
using ENTIDAD.GENERICS;
using System;

namespace NEGOCIO
{
    public class UsuarioCN : Singleton<UsuarioCN>
    {
        public Persona obtener_por_id(string userId)
        {
            try
            {
                return UsuarioDAL.Instancia.obtener_por_id(userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
