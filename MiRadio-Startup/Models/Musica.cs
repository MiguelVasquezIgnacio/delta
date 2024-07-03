using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiRadio_Startup.Models
{
    public class Musica
    {
        [Key]
        public int IdMusica { get; set; }

        [Required]
        public DateTime FechaPublicacion { get; set; }

        public string? Titulo { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "El autor debe tener al menos 5 caracteres.")]
        public string? Autor { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "El género debe tener al menos 5 caracteres.")]
        public string? Genero { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "La descripción debe tener al menos 5 caracteres.")]
        public string? Descripcion { get; set; }

        [Required]
        [Range(1, 15360, ErrorMessage = "El tamaño debe ser mayor que 0 y menor o igual a 15360 KB (15 MB).")]
        public int TamanoKB { get; set; } // Tamaño en KB

        public string? MusicaFilename { get; set; }

        [NotMapped]
        public IFormFile? MusicaFile { get; set; }

        // Propiedad de navegación para los comentarios
        public virtual List<Comentario>? Comentarios { get; set; }
       



        public override string ToString()
        {
            return $"{IdMusica}\n{FechaPublicacion}\n{Titulo}\n{Autor}\n{Genero}\n{Descripcion}\n{TamanoKB}";
        }
    }
}
