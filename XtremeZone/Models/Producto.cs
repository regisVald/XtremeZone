using System.ComponentModel.DataAnnotations.Schema;

namespace XtremeZone.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string ImagenUrl { get; set; }

        // Foreign Key
        public int CategoriaId { get; set; }

        // Navigation property
        [ForeignKey("CategoriaId")]
        public Categoria Categoria { get; set; }

        public DateTime FechaCreacion { get; set; }
    }
}
