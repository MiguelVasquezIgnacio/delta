using System.ComponentModel.DataAnnotations;

namespace MiRadio_Startup.Models
{
    public class Musica
    {
        [Key]
        public int IdMusica { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public string UrlMusica { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Genero { get; set; }
        public string Descripcion { get; set; }
        public float TamañoMB { get; set; }
        public List<string> Etiquetas { get; set; }
        public string CodigoQR { get; set; }
    }
}
