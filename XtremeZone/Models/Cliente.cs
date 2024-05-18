using System.ComponentModel.DataAnnotations;

namespace XtremeZone.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(50)]
        public string Apellido { get; set; }

        [Required]
        public int Edad { get; set; }

        [Required]
        [EmailAddress]
        public string Correo { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Contrasenia { get; set; }

        public DateTime FechaCreacion { get; set; }
    }
}
