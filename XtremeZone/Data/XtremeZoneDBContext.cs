using Microsoft.EntityFrameworkCore;
using XtremeZone.Models;

namespace XtremeZone.Data
{
    public class XtremeZoneDBContext : DbContext
    {
        public XtremeZoneDBContext(DbContextOptions<XtremeZoneDBContext> options) : base(options)
        {
            
        }
        public DbSet<Cliente>? Clientes { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Producto> Producto { get; set; }
        public DbSet<ProductoDestacado> ProductosDestacados { get; set; }
        public DbSet<OfertaEspecial> OfertasEspeciales { get; set; }
        public DbSet<Contacto> Contactos { get; set; }

        //public DbSet<EstadoPedido> EstadoPedidos { get; set; }
        //public DbSet<Pedido> Pedidos { get; set; }
        //public DbSet<DetallePedido> DetallePedidos { get; set; }
    }
}
