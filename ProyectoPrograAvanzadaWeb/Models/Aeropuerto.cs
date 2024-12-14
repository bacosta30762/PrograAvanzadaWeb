using System.ComponentModel.DataAnnotations;

namespace ProyectoPrograAvanzadaWeb.Models
{
    public class Aeropuerto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El código IATA es obligatorio.")]
        [StringLength(3, ErrorMessage = "El código IATA debe tener 3 caracteres.")]
        public string IATA { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "La ciudad es obligatoria.")]
        [StringLength(100, ErrorMessage = "La ciudad no puede exceder los 100 caracteres.")]
        public string Ciudad { get; set; } = string.Empty;

        [Required(ErrorMessage = "El país es obligatorio.")]
        [StringLength(100, ErrorMessage = "El país no puede exceder los 100 caracteres.")]
        public string Pais { get; set; } = string.Empty;
    }
}
