using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiRadio_Startup.Models
{
    public class Musica
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Configurar para autogenerar ID
        public int IdMusica { get; set; }

        [Required(ErrorMessage = "La fecha de publicación es obligatoria.")]
        public DateTime FechaPublicacion { get; set; }


        public string? Titulo { get; set; }

        [Required(ErrorMessage = "El campo Autor es obligatorio.")]
        [MinLength(5, ErrorMessage = "El autor debe tener al menos 5 caracteres.")]
        public string? Autor { get; set; }

        [Required(ErrorMessage = "El campo Género es obligatorio.")]
        [MinLength(5, ErrorMessage = "El género debe tener al menos 5 caracteres.")]
        public string? Genero { get; set; }

        [Required(ErrorMessage = "El campo Descripción es obligatorio.")]
        [MinLength(5, ErrorMessage = "La descripción debe tener al menos 5 caracteres.")]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "El tamaño del archivo es obligatorio.")]
        [Range(1, 15360, ErrorMessage = "El tamaño debe ser mayor que 0 y menor o igual a 15360 KB (15 MB).")]
        public int TamanoKB { get; set; } // Tamaño en KB

        public string? MusicaFilename { get; set; }

        [NotMapped]
        [Display(Name = "Subir música")]
        public IFormFile? MusicaFile { get; set; }

        public List<Etiqueta>? Etiquetas { get; set; }

        public override string ToString()
        {
            return IdMusica + "\n" + FechaPublicacion + "\n" + Titulo + "\n" + Autor + "\n" + Genero + "\n" + Descripcion + "\n" + TamanoKB; 
        }
    }
}
