using System.ComponentModel.DataAnnotations;

namespace ProyectoPrograAvanzadaWeb.ViewModel
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(10, ErrorMessage = "La cédula no puede tener más de 10 caracteres.")]
        public string Cedula { get; set; }

        [Required]
        [StringLength(200)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(200)]
        public string Apellidos { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "La contraseña y la confirmación no coinciden.")]
        public string ConfirmPassword { get; set; }
    }
}
