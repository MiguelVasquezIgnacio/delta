using Microsoft.AspNetCore.Mvc;
using MiRadio_Startup.Context;
using MiRadio_Startup.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MiRadio_Startup.Controllers
{
    public class LoginController : Controller
    {
        private readonly MyContext _context;

        public LoginController(MyContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Recuperar la lista de música de la base de datos
            var musicaList = _context.Musicas.ToList();

            // Pasar la lista de música a la vista
            return View(musicaList);
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var usuario = _context.Usuarios
                .Where(x => x.Email == email && x.Password == password)
                .FirstOrDefault();

            if (usuario != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["Login error"] = "Correo electrónico o contraseña incorrectos";
                return RedirectToAction("Index", "Login");
            }
        }

        public async Task<IActionResult> Logout()
        {
            return RedirectToAction("Index", "Login");
        }
    }
}
