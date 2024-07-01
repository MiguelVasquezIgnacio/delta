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
    public class ComentariosController : Controller
    {
        private readonly MyContext _context;

        public ComentariosController(MyContext context)
        {
            _context = context;
        }

        // GET: Comentarios
        public async Task<IActionResult> Index()
        {
            // Incluir Musica y Usuario en la consulta para cargar la información de las relaciones
            var myContext = _context.Comentarios.Include(c => c.MusicaS).Include(c => c.UsuarioS);
            return View(await myContext.ToListAsync());
        }

        // GET: Comentarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comentario = await _context.Comentarios
                .Include(c => c.MusicaS)
                .Include(c => c.UsuarioS)
                .FirstOrDefaultAsync(m => m.IdComentario == id);
            if (comentario == null)
            {
                return NotFound();
            }

            return View(comentario);
        }

        // GET: Comentarios/Create
        public IActionResult Create()
        {
            // Poblamos el ViewBag con las listas necesarias para los dropdowns
            ViewData["MusicaId"] = new SelectList(_context.Musicas, "IdMusica", "Titulo"); // Usamos "Titulo" para mostrar nombres más amigables
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Nombre");
            return View();
        }

        // POST: Comentarios/Create
        // Protegemos contra ataques de sobreposteo habilitando las propiedades específicas que queremos enlazar
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Create([Bind("Texto,UsuarioId,MusicaId")] Comentario comentario)
        {
            Console.WriteLine(comentario.Texto);
            Console.WriteLine(comentario.UsuarioId);
            Console.WriteLine(comentario.MusicaId);
           
            if (ModelState.IsValid)
            {
                Console.WriteLine("aqui");
                // Establecer la fecha de publicación a la fecha y hora actual
                comentario.FechaPublicacion = DateTime.Now;

                _context.Add(comentario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Poblar los dropdowns nuevamente en caso de error
            ViewData["MusicaId"] = new SelectList(_context.Musicas, "IdMusica", "Titulo", comentario.MusicaId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Nombre", comentario.UsuarioId);
            return View(comentario);
        }

        // GET: Comentarios/Edit/5
        // GET: Comentarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comentario = await _context.Comentarios
                .Include(c => c.MusicaS)
                .Include(c => c.UsuarioS)
                .FirstOrDefaultAsync(c => c.IdComentario == id);

            if (comentario == null)
            {
                return NotFound();
            }

            // Poblar los dropdowns con los valores actuales seleccionados
            ViewData["MusicaId"] = new SelectList(_context.Musicas, "IdMusica", "Titulo", comentario.MusicaId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Nombre", comentario.UsuarioId);

            return View(comentario);
        }


        // POST: Comentarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdComentario,Texto,UsuarioId,MusicaId")] Comentario comentario)
        {
            Console.WriteLine(comentario.Texto);
            Console.WriteLine(comentario.UsuarioId);
            Console.WriteLine(comentario.MusicaId);
           
            if (id != comentario.IdComentario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comentario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComentarioExists(comentario.IdComentario))
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
            ViewData["MusicaId"] = new SelectList(_context.Musicas, "IdMusica", "Titulo", comentario.MusicaId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Nombre", comentario.UsuarioId);
            return View(comentario);
        }

        // GET: Comentarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comentario = await _context.Comentarios
                .Include(c => c.MusicaS)
                .Include(c => c.UsuarioS)
                .FirstOrDefaultAsync(m => m.IdComentario == id);
            if (comentario == null)
            {
                return NotFound();
            }

            return View(comentario);
        }

        // POST: Comentarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comentario = await _context.Comentarios.FindAsync(id);
            if (comentario != null)
            {
                _context.Comentarios.Remove(comentario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComentarioExists(int id)
        {
            return _context.Comentarios.Any(e => e.IdComentario == id);
        }
    }
}
