using System.ComponentModel.DataAnnotations;

namespace ProyectoPrograAvanzadaWeb.ViewModel
{
    public class RecuperarPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Correo { get; set; }
    }
}
