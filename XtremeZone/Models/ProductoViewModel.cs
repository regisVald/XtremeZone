namespace XtremeZone.Models
{
    namespace XtremeZone.Models
    {
        public class ProductoViewModel
        {
            public IEnumerable<Producto> Productos { get; set; }
            //public IEnumerable<Categoria> Categorias { get; set; }
            public int CategoriaSeleccionada { get; set; }
            public decimal? PrecioMin { get; set; }
            public decimal? PrecioMax { get; set; }
            public string OrdenarPor { get; set; }
            public List<Categoria> Categorias { get; set; }
            public List<Producto> ProductosDestacados { get; set; }
            public List<Producto> OfertasEspeciales { get; set; }
            public List<Producto> NuevosProductos { get; set; }
        }
    }

}
