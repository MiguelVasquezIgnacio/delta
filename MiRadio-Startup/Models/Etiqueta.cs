using System.ComponentModel.DataAnnotations;

namespace MiRadio_Startup.Models
{
    public class Etiqueta
    {
        [Key]
        public int IdEtiqueta { get; set; }
        public string? Valor { get; set; }

        public int MusicaId { get; set; }
        public Musica? Musica { get; set; }
    }
}
