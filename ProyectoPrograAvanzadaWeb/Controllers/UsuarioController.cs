using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProyectoPrograAvanzadaWeb.Models;
using ProyectoPrograAvanzadaWeb.Services;
using ProyectoPrograAvanzadaWeb.ViewModel;

namespace ProyectoPrograAvanzadaWeb.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly IEnviadorCorreos _enviadorCorreos;

        public UsuarioController(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, IEnviadorCorreos enviadorCorreos)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _enviadorCorreos = enviadorCorreos;
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
                var user = new Usuario
                {
                    Cedula = model.Cedula,
                    Nombre = model.Nombre,
                    Apellidos = model.Apellidos,
                    UserName = model.Email,
                    Email = model.Email,
                    Activo = true
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                    return RedirectToAction("Login", "Usuario");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
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
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                if (result.IsLockedOut)
                {
                    return RedirectToAction("Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
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
                var usuario = await _userManager.FindByEmailAsync(model.Correo);
                if (usuario != null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(usuario);
                    var link = Url.Action("RestablecerPassword", "Usuario",
                        new { token, correo = usuario.Email }, Request.Scheme);

                    await _enviadorCorreos.SendEmailAsync(usuario.Email,
                        "Recuperación de contraseña",
                        $"Haga clic en el siguiente enlace para restablecer su contraseña: <a href='{link}'>Restablecer contraseña</a>");

                    TempData["Mensaje"] = "Se ha enviado un correo para restablecer la contraseña.";
                    return RedirectToAction("Login");
                }

                ModelState.AddModelError(string.Empty, "El correo no está registrado.");
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
                var usuario = await _userManager.FindByEmailAsync(model.Correo);
                if (usuario != null)
                {
                    var resultado = await _userManager.ResetPasswordAsync(usuario, model.Token, model.NuevaPassword);
                    if (resultado.Succeeded)
                    {
                        TempData["Mensaje"] = "La contraseña se ha restablecido correctamente.";
                        return RedirectToAction("Login");
                    }

                    foreach (var error in resultado.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "El correo no está registrado.");
                }
            }

            return View(model);
        }
    }
}

