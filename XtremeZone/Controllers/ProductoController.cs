using Microsoft.AspNetCore.Mvc;
using XtremeZone.Data;

namespace XtremeZone.Controllers
{
    public class ProductoController : Controller
    {
        public readonly XtremeZoneDBContext _context;

        public ProductoController(XtremeZoneDBContext context)
        {
            _context = context; 
        }
        public IActionResult Index()
        {
            var productos = _context.Producto.ToList();
            return View(productos);
        }
    }
}
