using Microsoft.AspNetCore.Mvc;
using XtremeZone.Data;
using XtremeZone.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace XtremeZone.Controllers
{
    public class CarritoController : Controller
    {
        private readonly XtremeZoneDBContext _context;
        private const string CarritoSessionKey = "Carrito";

        public CarritoController(XtremeZoneDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> AgregarAlCarrito(int id)
        {
            var producto = await _context.Producto.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            var carrito = ObtenerCarritoDeSession();
            var item = carrito.Items.FirstOrDefault(i => i.producto.Id == id);

            if (item == null)
            {
                carrito.Items.Add(new CarritoItem { producto = producto, Cantidad = 1 });
            }
            else
            {
                item.Cantidad++;
            }

            GuardarCarritoEnSession(carrito);

            return RedirectToAction("Index", "Producto");
        }

        public IActionResult VerCarrito()
        {
            var carrito = ObtenerCarritoDeSession();
            return View(carrito);
        }

        private CarritoViewModel ObtenerCarritoDeSession()
        {
            var carrito = HttpContext.Session.Get<CarritoViewModel>(CarritoSessionKey) ?? new CarritoViewModel();
            return carrito;
        }

        private void GuardarCarritoEnSession(CarritoViewModel carrito)
        {
            HttpContext.Session.Set(CarritoSessionKey, carrito);
        }
    }
}
