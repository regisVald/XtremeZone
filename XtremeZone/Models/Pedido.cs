using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XtremeZone.Models
{
    public class Pedido
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public DateTime FechaPedido { get; set; }

        [Required]
        public int ClienteId { get; set; }

        // Navigation property
        public Cliente Cliente { get; set; }

        // Navigation property
        public List<DetallePedido> Detalles { get; set; }
    }

}
