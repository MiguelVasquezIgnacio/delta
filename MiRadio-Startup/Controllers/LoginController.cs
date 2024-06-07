using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiRadio_Startup.Context;
using SQLitePCL;

namespace MiRadio_Startup.Controllers
{
    public class LoginController : Controller
    {
        MyContext _context;

        public LoginController(MyContext context)
        {
            this._context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Login(string email, string password)
        {
            var usuario = _context.Usuarios
                .Where(x => x.email == email)
                .Where(x => x.password == password)
                .FirstOrDefault();

            if (usuario != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {

                TempData["Login error"] = " cuenta o password incorrecta";
                return RedirectToAction("index", "Login");
            }
            }

            public async Task<IActionResult> Logout()
            {
                return RedirectToAction("Index", "Login");
            }
        
      }
}
