using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiRadio_Startup.Context;
using MiRadio_Startup.Models;

namespace MiRadio_Startup.Controllers
{
    public class MusicasController : Controller
    {
        private readonly MyContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MusicasController(MyContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Musicas
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            var musicas = _context.Musicas.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                musicas = musicas.Where(m => m.Titulo.Contains(searchString) ||
                                             m.Autor.Contains(searchString) ||
                                             m.Genero.Contains(searchString));
            }

            return View(await musicas.ToListAsync());
        }

        // GET: Musicas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var musica = await _context.Musicas
                .Include(m => m.Comentarios) // Incluir los comentarios asociados
                .ThenInclude(c => c.UsuarioS) // Incluir información del usuario que hizo el comentario
                .FirstOrDefaultAsync(m => m.IdMusica == id);

            if (musica == null)
            {
                return NotFound();
            }

            return View(musica);
        }

        // GET: Musicas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Musicas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FechaPublicacion,Titulo,Autor,Genero,Descripcion,TamanoKB,MusicaFile")] Musica musica)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (musica.MusicaFile != null && musica.MusicaFile.Length > 0)
                    {
                        var tamanoKB = (int)(musica.MusicaFile.Length / 1024.0);
                        musica.TamanoKB = tamanoKB;

                        var fileSaveResult = await GuardarMusicaAsync(musica);
                        if (!fileSaveResult.Success)
                        {
                            ModelState.AddModelError("MusicaFile", fileSaveResult.ErrorMessage);
                            return View(musica);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("MusicaFile", "Debe seleccionar un archivo.");
                        return View(musica);
                    }

                    _context.Add(musica);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Ocurrió un error al intentar guardar la música.");
                    // Log the error (ex.Message)
                    return View(musica);
                }
            }
            return View(musica);
        }

        // GET: Musicas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var musica = await _context.Musicas.FirstOrDefaultAsync(m => m.IdMusica == id);

            if (musica == null)
            {
                return NotFound();
            }

            return View(musica);
        }

        // POST: Musicas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdMusica,FechaPublicacion,Titulo,Autor,Genero,Descripcion,TamanoKB,MusicaFile")] Musica musica)
        {
            if (id != musica.IdMusica)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var musicaToUpdate = await _context.Musicas.FirstOrDefaultAsync(m => m.IdMusica == id);

                    if (musicaToUpdate == null)
                    {
                        return NotFound();
                    }

                    if (musica.MusicaFile != null)
                    {
                        var fileSaveResult = await GuardarMusicaAsync(musica);
                        if (!fileSaveResult.Success)
                        {
                            ModelState.AddModelError("MusicaFile", fileSaveResult.ErrorMessage);
                            return View(musica);
                        }
                    }

                    musicaToUpdate.FechaPublicacion = musica.FechaPublicacion;
                    musicaToUpdate.Titulo = musica.Titulo;
                    musicaToUpdate.Autor = musica.Autor;
                    musicaToUpdate.Genero = musica.Genero;
                    musicaToUpdate.Descripcion = musica.Descripcion;
                    musicaToUpdate.TamanoKB = musica.TamanoKB;

                    _context.Update(musicaToUpdate);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Ocurrió un error al intentar actualizar la música.");
                    // Log the error (ex.Message)
                    return View(musica);
                }

            }

            return View(musica);
        }

        // GET: Musicas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var musica = await _context.Musicas.FirstOrDefaultAsync(m => m.IdMusica == id);

            if (musica == null)
            {
                return NotFound();
            }

            return View(musica);
        }

        // POST: Musicas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var musica = await _context.Musicas.FindAsync(id);

            if (musica != null)
            {
                var deleteFileResult = EliminarMusicaArchivo(musica.MusicaFilename);

                if (!deleteFileResult.Success)
                {
                    // Handle error if needed
                    // ModelState.AddModelError("", deleteFileResult.ErrorMessage);
                }

                _context.Musicas.Remove(musica);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<FileOperationResult> GuardarMusicaAsync(Musica musica)
        {
            try
            {
                if (musica.MusicaFile != null && musica.MusicaFile.Length > 0)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    string extension = Path.GetExtension(musica.MusicaFile.FileName);
                    string nameMusica = $"{Guid.NewGuid()}{extension}";
                    string path = Path.Combine(wwwRootPath, "musicas", nameMusica);
                    Directory.CreateDirectory(Path.GetDirectoryName(path));

                    musica.MusicaFilename = nameMusica;

                    using (var fileStream = new FileStream(path, FileMode.Create))

                    {
                        await musica.MusicaFile.CopyToAsync(fileStream);
                    }

                    return new FileOperationResult { Success = true };
                }
                return new FileOperationResult { Success = false, ErrorMessage = "El archivo de música no es válido." };
            }
            catch (Exception ex)
            {
                // Log the error (ex.Message)
                return new FileOperationResult { Success = false, ErrorMessage = ex.Message };
            }
        }

        private FileOperationResult EliminarMusicaArchivo(string filename)
        {
            try
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string path = Path.Combine(wwwRootPath, "musicas", filename);

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                    return new FileOperationResult { Success = true };
                }
                return new FileOperationResult { Success = false, ErrorMessage = "El archivo no existe." };
            }
            catch (Exception ex)
            {
                // Log the error (ex.Message)
                return new FileOperationResult { Success = false, ErrorMessage = ex.Message };
            }
        }

        private class FileOperationResult
        {
            public bool Success { get; set; }
            public string? ErrorMessage { get; set; }
        }
    }
}
