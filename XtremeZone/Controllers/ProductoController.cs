using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XtremeZone.Data;
using XtremeZone.Models.XtremeZone.Models;
using System.Linq;
using System.Threading.Tasks;

namespace XtremeZone.Controllers
{
    public class ProductoController : Controller
    {
        private readonly XtremeZoneDBContext _context;

        public ProductoController(XtremeZoneDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? categoriaId, decimal? precioMin, decimal? precioMax, string ordenarPor)
        {
            // Obtener todos los productos con sus categorías
            var productosQuery = _context.Producto.Include(p => p.Categoria).AsQueryable();

            // Filtrar por categoría si se proporciona
            if (categoriaId.HasValue && categoriaId.Value > 0)
            {
                productosQuery = productosQuery.Where(p => p.CategoriaId == categoriaId.Value);
            }

            // Filtrar por precio mínimo si se proporciona
            if (precioMin.HasValue)
            {
                productosQuery = productosQuery.Where(p => p.Precio >= precioMin.Value);
            }

            // Filtrar por precio máximo si se proporciona
            if (precioMax.HasValue)
            {
                productosQuery = productosQuery.Where(p => p.Precio <= precioMax.Value);
            }

            // Ordenar los productos según la opción seleccionada
            switch (ordenarPor)
            {
                case "Nombre":
                    productosQuery = productosQuery.OrderBy(p => p.Nombre);
                    break;
                case "PrecioAsc":
                    productosQuery = productosQuery.OrderBy(p => p.Precio);
                    break;
                case "PrecioDesc":
                    productosQuery = productosQuery.OrderByDescending(p => p.Precio);
                    break;
                default:
                    productosQuery = productosQuery.OrderBy(p => p.Nombre);
                    break;
            }

            // Obtener los productos destacados
            var productosDestacados = await _context.ProductosDestacados
                .Select(pd => pd.Producto)
                .Take(5)
                .ToListAsync();

            // Obtener los productos en oferta especial
            var ofertasEspeciales = await _context.OfertasEspeciales
                .Select(oe => oe.Producto)
                .Take(5)
                .ToListAsync();

            // Crear el modelo de vista con los productos filtrados y otras propiedades
            var viewModel = new ProductoViewModel
            {
                Categorias = await _context.Categorias.ToListAsync(),
                Productos = await productosQuery.ToListAsync(),
                CategoriaSeleccionada = categoriaId ?? 0,
                PrecioMin = precioMin,
                PrecioMax = precioMax,
                OrdenarPor = ordenarPor,
                ProductosDestacados = productosDestacados,
                OfertasEspeciales = ofertasEspeciales
            };

            // Devolver la vista con el modelo de vista
            return View(viewModel);
        }
    }
}
