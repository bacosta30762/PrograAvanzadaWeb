using Microsoft.AspNetCore.Identity;
using ProyectoPrograAvanzadaWeb.Models;

namespace ProyectoPrograAvanzadaWeb.Seeder
{
    public class RoleSeeder
    {
        public static async Task Seed(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<Usuario>>();

            string[] roleNames = { "Admin", "User" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(new Role(roleName));
                }
            }

            var user = await userManager.FindByEmailAsync("admin@example.com");

            if (user == null)
            {
                user = new Usuario()
                {
                    Nombre = "Admin",
                    UserName = "correo@admin.com",
                    Email = "correo@admin.com",
                };
                await userManager.CreateAsync(user, "Admin123!");
            }

            await userManager.AddToRoleAsync(user, "Admin");
        }
    }
}
