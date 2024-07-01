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
            var musicas = from m in _context.Musicas
                          select m;

            if (!string.IsNullOrEmpty(searchString))
            {
                musicas = musicas.Where(s => s.Titulo.Contains(searchString));
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
                if (musica.MusicaFile != null && musica.MusicaFile.Length > 0)
                {
                    var tamanoKB = (int)(musica.MusicaFile.Length / 1024.0);
                    musica.TamanoKB = tamanoKB;

                    // Guardar archivo de música
                    var fileSaveResult = await GuardarMusicaAsync(musica);
                    if (!fileSaveResult.Success)
                    {
                        ModelState.AddModelError("MusicaFile", fileSaveResult.ErrorMessage);
                    }
                    else
                    {
                        // Guardar datos en la base de datos
                        _context.Add(musica);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
                else
                {
                    ModelState.AddModelError("MusicaFile", "Debe seleccionar un archivo.");
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

            var musica = await _context.Musicas.FindAsync(id);
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
                    if (musica.MusicaFile != null)
                    {
                        var fileSaveResult = await GuardarMusicaAsync(musica);
                        if (!fileSaveResult.Success)
                        {
                            ModelState.AddModelError("MusicaFile", fileSaveResult.ErrorMessage);
                            return View(musica);
                        }
                    }

                    _context.Update(musica);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MusicaExists(musica.IdMusica))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
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
                // Eliminar archivo físico si existe
                var deleteFileResult = EliminarMusicaArchivo(musica.MusicaFilename);
                if (!deleteFileResult.Success)
                {
                    // Manejar el error si es necesario
                    // ModelState.AddModelError("", deleteFileResult.ErrorMessage);
                }

                _context.Musicas.Remove(musica);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool MusicaExists(int id)
        {
            return _context.Musicas.Any(e => e.IdMusica == id);
        }

        private async Task<FileOperationResult> GuardarMusicaAsync(Musica musica)
        {
            try
            {
                if (musica.MusicaFile != null && musica.MusicaFile.Length > 0)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    string extension = Path.GetExtension(musica.MusicaFile.FileName);

                    // Generar nombre único para el archivo
                    string nameMusica = $"{Guid.NewGuid()}{extension}";

                    // Definir la ruta completa del archivo
                    string path = Path.Combine(wwwRootPath, "musicas", nameMusica);

                    // Crear directorio si no existe
                    Directory.CreateDirectory(Path.GetDirectoryName(path));

                    musica.MusicaFilename = nameMusica;

                    // Guardar el archivo físicamente
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
            public string ErrorMessage { get; set; }
        }
    }
}
