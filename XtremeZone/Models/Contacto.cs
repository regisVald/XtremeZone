using System.ComponentModel.DataAnnotations;

namespace XtremeZone.Models
{
    public class Contacto
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        [EmailAddress]
        public string CorreoElectronico { get; set; }

        [Required]
        public string Mensaje { get; set; }
    }
}
