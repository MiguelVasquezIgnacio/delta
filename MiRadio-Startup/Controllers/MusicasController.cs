using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public MusicasController(MyContext context)
        {
            _context = context;
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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdMusica,FechaPublicacion,Titulo,Autor,Genero,Descripcion,TamañoMB,UrlMusica")] Musica musica)
        {
            if (ModelState.IsValid)
            {
                _context.Add(musica);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdMusica,FechaPublicacion,Titulo,Autor,Genero,Descripcion,TamañoMB,UrlMusica")] Musica musica)
        {
            if (id != musica.IdMusica)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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
