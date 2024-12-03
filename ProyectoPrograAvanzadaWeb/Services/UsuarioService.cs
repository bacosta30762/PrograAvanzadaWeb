using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using ProyectoPrograAvanzadaWeb.Models;
using ProyectoPrograAvanzadaWeb.ViewModel;

namespace ProyectoPrograAvanzadaWeb.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly IEnviadorCorreos _enviadorCorreos;
        private readonly IUrlHelper _urlHelper;

        public UsuarioService(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, IEnviadorCorreos enviadorCorreos, IHttpContextAccessor httpContextAccessor,
            IUrlHelperFactory urlHelperFactory)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _enviadorCorreos = enviadorCorreos;

            var httpContext = httpContextAccessor.HttpContext;
            var actionContext = new ActionContext(httpContext, new Microsoft.AspNetCore.Routing.RouteData(), new Microsoft.AspNetCore.Mvc.Abstractions.ActionDescriptor());
            _urlHelper = urlHelperFactory.GetUrlHelper(actionContext);
        }

        public async Task<string> RegistrarUsuario(RegisterViewModel model)
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
                return "Success";
            }

            foreach (var error in result.Errors)
            {
                return error.Description;
            }
            return "Error";
        }

        public async Task<string> LoginUsuario(LoginViewModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return "Success";
            }

            if (result.IsLockedOut)
            {
                return "LockedOut";
            }
            return "InvalidLogin";
        }

        public async Task<string> RecuperarPassword(RecuperarPasswordViewModel model)
        {
            var usuario = await _userManager.FindByEmailAsync(model.Correo);
            if (usuario != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(usuario);
                var link = _urlHelper.Action("RestablecerPassword", "Usuario", new { token, correo = usuario.Email }, "https");
                await _enviadorCorreos.SendEmailAsync(usuario.Email, "Recuperación de contraseña", $"Haga clic en el siguiente enlace para restablecer su contraseña: <a href='{link}'>Restablecer contraseña</a>");
                return "CorreoEnviado";
            }
            return "CorreoNoRegistrado";
        }

        public async Task<string> RestablecerPassword(RestablecerPasswordViewModel model)
        {
            var usuario = await _userManager.FindByEmailAsync(model.Correo);
            if (usuario != null)
            {
                var result = await _userManager.ResetPasswordAsync(usuario, model.Token, model.NuevaPassword);
                if (result.Succeeded)
                {
                    return "Success";
                }

                foreach (var error in result.Errors)
                {
                    return error.Description;
                }
            }
            return "CorreoNoRegistrado";
        }
    }
}
