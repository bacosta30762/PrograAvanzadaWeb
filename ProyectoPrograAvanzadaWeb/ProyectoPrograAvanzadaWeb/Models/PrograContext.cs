using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ProyectoPrograAvanzadaWeb.Models
{
    public class PrograContext : IdentityDbContext<Usuario, Role, string>

    {
        public PrograContext(DbContextOptions<PrograContext> options) : base(options)
        {
        }
    }
}
