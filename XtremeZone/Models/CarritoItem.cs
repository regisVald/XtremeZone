namespace XtremeZone.Models
{
    public class CarritoItem
    {
        public int Cantidad {  get; set; }
        public Producto producto { get; set; }
    }

    public class CarritoViewModel
    {
        public List<CarritoItem> Items { get; set; } = new List<CarritoItem>();
        public decimal Total => Items.Sum(item => item.producto.Precio * item.Cantidad);
    }
}
