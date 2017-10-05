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
   public  class PersonaTipoCN:Singleton<PersonaTipoCN>
    {
       public List<PersonaTipo> listar()
       {
           try
           {
               return PersonaTipoDAL.Instancia.listar();
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }
       }

    }
}
