using ProyectoPrograAvanzadaWeb.ViewModel;

namespace ProyectoPrograAvanzadaWeb.Services
{
    public interface IUsuarioService
    {
        Task<string> RegistrarUsuario(RegisterViewModel model);
        Task<string> LoginUsuario(LoginViewModel model);
        Task<string> RecuperarPassword(RecuperarPasswordViewModel model);
        Task<string> RestablecerPassword(RestablecerPasswordViewModel model);
    }
}
