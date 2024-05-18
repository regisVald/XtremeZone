namespace XtremeZone.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }

        // Navigation property
        public ICollection<Producto> Productos { get; set; }
    }
}
