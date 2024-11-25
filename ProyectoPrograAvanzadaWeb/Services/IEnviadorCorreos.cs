
namespace ProyectoPrograAvanzadaWeb.Services
{
    public interface IEnviadorCorreos
    {
        Task SendEmailAsync(string to, string subject, string body);
    }
}
