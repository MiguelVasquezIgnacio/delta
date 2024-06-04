using Microsoft.AspNetCore.Identity;
using MiRadio_Startup.Dtos;
using System.ComponentModel.DataAnnotations;

namespace MiRadio_Startup.Models
{


    public class Usuario
    {
        [Key]//anotacion
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string email { get; set; }

        public string NombreUsuario { get; set; }

        public string password { get; set; }

        public RolEnum Rol { get; set; }

        //relaciones 
        public virtual List<Comentario> Comentarios { get; set; }
    }
}
