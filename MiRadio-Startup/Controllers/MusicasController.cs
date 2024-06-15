using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiRadio_Startup.Context;
using MiRadio_Startup.Models;


namespace MiRadio_Startup.Controllers
{
    public class MusicasController : Controller
    {
        private readonly MyContext _context;
        IWebHostEnvironment _webHostEnvironment;

        public MusicasController(MyContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Musicas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Musicas.ToListAsync());
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
        public async Task<IActionResult> Create([Bind("FechaPublicacion,Titulo,Autor,Genero,Descripcion,UrlMusica,MusicaFile")] Musica musica)
        {
            if (musica.MusicaFile != null && musica.MusicaFile.Length > 0)
            {
                // Calcular el tamaño del archivo en KB
                var tamañoKB = (int)(musica.MusicaFile.Length / 1024.0); // Convertir bytes a kilobytes
                musica.TamanoKB = tamañoKB;

                // Aquí podrías guardar el archivo si es necesario, por ejemplo, en el sistema de archivos o en un almacenamiento en la nube

                if (ModelState.IsValid)
                {
                    _context.Add(musica);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                ModelState.AddModelError("MusicaFile", "Debe seleccionar un archivo.");
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
        public async Task<IActionResult> Edit(int id, Musica musica)
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
                       await GuardarMusica(musica);
                    }

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

        private async Task GuardarMusica(Musica musica)
        {
            
                
                string wwwRootPath = _webHostEnvironment.WebRootPath;
              
                string extension = Path.GetExtension(musica.MusicaFile!.FileName);
              
                string nameMusica = $"{musica.IdMusica}_{musica.Titulo}{extension}";

            musica.UrlMusica = nameMusica;

            string path = Path.Combine($"{wwwRootPath}/musicas/", nameMusica);
            var fileStream =new FileStream (path, FileMode.Create);
            await musica.MusicaFile.CopyToAsync(fileStream);
        }

        // GET: Musicas/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Musicas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var musica = await _context.Musicas.FindAsync(id);
            if (musica != null)
            {
                _context.Musicas.Remove(musica);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MusicaExists(int id)
        {
            return _context.Musicas.Any(e => e.IdMusica == id);
        }
    }
}
