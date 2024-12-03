using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProyectoPrograAvanzadaWeb.Models;
using ProyectoPrograAvanzadaWeb.Services;
using ProyectoPrograAvanzadaWeb.ViewModel;

namespace ProyectoPrograAvanzadaWeb.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        private readonly SignInManager<Usuario> _signInManager;

        public UsuarioController(IUsuarioService usuarioService, SignInManager<Usuario> signInManager)
        {
            _usuarioService = usuarioService;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _usuarioService.RegistrarUsuario(model);
                if (result == "Success")
                {
                    return RedirectToAction("Login", "Usuario");
                }
                ModelState.AddModelError(string.Empty, result);
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _usuarioService.LoginUsuario(model);
                if (result == "Success")
                {
                    return RedirectToAction("Index", "Home");
                }

                if (result == "LockedOut")
                {
                    return RedirectToAction("Lockout");
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Lockout()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Usuario");
        }

        [HttpGet]
        public IActionResult RecuperarPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RecuperarPassword(RecuperarPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _usuarioService.RecuperarPassword(model);
                if (result == "CorreoEnviado")
                {
                    TempData["Mensaje"] = "Se ha enviado un correo para restablecer la contraseña.";
                    return RedirectToAction("Login");
                }

                ModelState.AddModelError(string.Empty, result);
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult RestablecerPassword(string token, string correo)
        {
            if (token == null || correo == null)
            {
                return RedirectToAction("Login");
            }

            var model = new RestablecerPasswordViewModel { Token = token, Correo = correo };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RestablecerPassword(RestablecerPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _usuarioService.RestablecerPassword(model);
                if (result == "Success")
                {
                    TempData["Mensaje"] = "La contraseña se ha restablecido correctamente.";
                    return RedirectToAction("Login");
                }

                ModelState.AddModelError(string.Empty, result);
            }
            return View(model);
        }
    }
}

