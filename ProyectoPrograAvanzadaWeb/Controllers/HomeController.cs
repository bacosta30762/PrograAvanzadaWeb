using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoPrograAvanzadaWeb.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
                ViewData["Message"] = "Bienvenido Administrador";
            }
            else if (User.IsInRole("User"))
            {
                ViewData["Message"] = "Bienvenido Usuario";
            }
            else
            {
                ViewData["Message"] = "Bienvenido Invitado";
            }
            return View();
        }
    }
}

