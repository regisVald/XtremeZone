namespace XtremeZone.Models
{
    public class OfertaEspecial
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }
    }
}
