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

    // Validación para asegurarse de que se proporciona una fecha de publicación.
    [Required]
    public DateTime FechaPublicacion { get; set; }

    // No necesita validaciones específicas, ya que se puede permitir que sea nulo.
    public string? Titulo { get; set; }

    // Validar que Autor no sea nulo y tenga al menos 5 caracteres.
    [Required, MinLength(5)]
    public string? Autor { get; set; }

    // Validar que Genero no sea nulo y tenga al menos 5 caracteres.
    [Required, MinLength(5)]
    public string? Genero { get; set; }

    // Validar que Descripcion no sea nula y tenga al menos 5 caracteres.
    [Required, MinLength(5)]
    public string? Descripcion { get; set; }

    // Validar que TamañoMB no sea cero y esté en un rango lógico. Aquí asumimos un rango razonable.
    [Required, Range(0.1, 15.0, ErrorMessage = "El tamaño debe ser mayor que 0 y menor o igual a 15 MB")]
    public float TamañoMB { get; set; }

    // No necesita validaciones específicas, ya que se puede permitir que sea nulo.
    public string UrlMusica { get; set; }


       [NotMapped]
        [Display (Name = "subir musica")]
        public IFormFile? MusicaFile { get; set; }

        public List<Etiqueta>? Etiquetas { get; set; }
    }
}
