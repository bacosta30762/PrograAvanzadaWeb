using Microsoft.AspNetCore.Identity;

namespace ProyectoPrograAvanzadaWeb.Models
{
    public class Role : IdentityRole
    {
        public Role() { }

        public Role(string roleName) : base(roleName) { }
    }
}
