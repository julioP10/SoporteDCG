using ENTIDAD;

namespace WEB.Models
{
    public class UsuarioModel
    {
        public Persona persona { get; set; }
        public UsuarioModel(Persona persona)
        {
            this.persona = persona;
        }
    }
}