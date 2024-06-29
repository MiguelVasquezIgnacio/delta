using System.ComponentModel.DataAnnotations;

using System;
using System.ComponentModel.DataAnnotations;

namespace MiRadio_Startup.Models
{
    public class Comentario
    {
        [Key]
        public int IdComentario { get; set; }
       
        public string? Texto { get; set; }
     
        public DateTime FechaPublicacion { get; set; }

        // Relaciones
        public int UsuarioId { get; set; }
        public int MusicaId { get; set; }

        public virtual Usuario? UsuarioS { get; set; }
        public virtual Musica? MusicaS { get; set; }
    }
}

