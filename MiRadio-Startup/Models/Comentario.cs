using System.ComponentModel.DataAnnotations;

namespace MiRadio_Startup.Models
{
    public class Comentario
    {
        [Key]
        public int IdComentario { get; set; }
        public string Texto { get; set; }
        public int IdUsuario { get; set; }
        public DateTime FechaPublicacion { get; set; }

        //relaciones
        public int Usuario { get; set; }

        public int Musica { get; set; }

        public virtual Usuario? UsuarioS { get; set; }
         
        public virtual Musica? MusicaS { get; set; }
    }
}
