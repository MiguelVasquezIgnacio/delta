using Microsoft.AspNetCore.Identity;
using MiRadio_Startup.Dtos;
using System.ComponentModel.DataAnnotations;

namespace MiRadio_Startup.Models
{


    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        [Required,MinLength(5)]
        public string? Nombre { get; set; }
        [Required, MinLength(4)]
        public string? Email { get; set; }
        [Required, MinLength(3)]
        public string? NombreUsuario { get; set; }
        [Required, MinLength(4)]
        public string? Password { get; set; }
        
        public RolEnum Rol { get; set; }

        //relaciones 
        public virtual List<Comentario>? Comentarios { get; set; }
       
    }
}
