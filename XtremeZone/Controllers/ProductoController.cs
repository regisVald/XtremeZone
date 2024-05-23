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
            var productosQuery = _context.Producto.Include(p => p.Categoria).AsQueryable();

            if (categoriaId.HasValue && categoriaId.Value > 0)
            {
                productosQuery = productosQuery.Where(p => p.CategoriaId == categoriaId.Value);
            }

            if (precioMin.HasValue)
            {
                productosQuery = productosQuery.Where(p => p.Precio >= precioMin.Value);
            }

            if (precioMax.HasValue)
            {
                productosQuery = productosQuery.Where(p => p.Precio <= precioMax.Value);
            }

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

            var viewModel = new ProductoViewModel
            {
                Categorias = await _context.Categorias.ToListAsync(),
                Productos = await productosQuery.ToListAsync(),
                CategoriaSeleccionada = categoriaId ?? 0,
                PrecioMin = precioMin,
                PrecioMax = precioMax,
                OrdenarPor = ordenarPor
            };

            return View(viewModel);
        }
    }
}
