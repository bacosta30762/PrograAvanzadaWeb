using System.ComponentModel.DataAnnotations;

namespace ProyectoPrograAvanzadaWeb.ViewModel
{
    public class RestablecerPasswordViewModel
    {
        [Required]
        public string Correo { get; set; }

        [Required]
        public string Token { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string NuevaPassword { get; set; }
    }
}
