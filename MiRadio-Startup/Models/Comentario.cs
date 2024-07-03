using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiRadio_Startup.Models
{
    public class Comentario
    {
        [Key]
        public int IdComentario { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "El comentario debe tener al menos 5 caracteres.")]
        public string? Texto { get; set; }

        public DateTime FechaPublicacion { get; set; } = DateTime.Now;

        // Relaciones
        [Required]
        public int UsuarioId { get; set; }

        [Required]
        public int MusicaId { get; set; }

        [ForeignKey("UsuarioId")]
        public virtual Usuario? UsuarioS { get; set; }

        [ForeignKey("MusicaId")]
        public virtual Musica? MusicaS { get; set; }
       
    }
}
