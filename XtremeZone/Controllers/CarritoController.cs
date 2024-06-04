using Microsoft.AspNetCore.Mvc;
using XtremeZone.Data;
using XtremeZone.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;

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

        public async Task<IActionResult> ProcesarOrden()
        {
            // Obtener el id del cliente actual
            int clienteId = ObtenerIdClienteActual();

            // Crear una nueva instancia de Pedido
            Pedido pedido = new Pedido
            {
                FechaPedido = DateTime.Now,
                ClienteId = clienteId,
                Detalles = new List<DetallePedido>()
            };

            // Guardar el pedido en la base de datos
            _context.Pedido.Add(pedido);
            await _context.SaveChangesAsync();

            // Obtener el carrito de la sesión
            var carrito = ObtenerCarritoDeSession();

            // Crear los detalles de pedido para cada producto en el carrito
            foreach (var item in carrito.Items)
            {
                var detallePedido = new DetallePedido
                {
                    PedidoId = pedido.Id,
                    ProductoId = item.producto.Id,
                    Cantidad = item.Cantidad,
                    PrecioUnitario = item.producto.Precio
                };

                // Agregar el detalle de pedido al pedido
                pedido.Detalles.Add(detallePedido);

                // Guardar el detalle de pedido en la base de datos
                _context.DetallePedido.Add(detallePedido);
            }

            // Guardar los cambios en la base de datos
            await _context.SaveChangesAsync();

            // Limpiar el carrito de la sesión
            LimpiarCarritoDeSession();

            // Redirigir a una vista de confirmación de pedido
            return RedirectToAction("ConfirmacionPedido");
        }

        private int ObtenerIdClienteActual()
        {
            // Implementa este método según tu lógica de autenticación
            return 1; // Ejemplo: siempre devuelve el id 1
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

        private void LimpiarCarritoDeSession()
        {
            HttpContext.Session.Remove(CarritoSessionKey);
        }
        public IActionResult ConfirmacionPedido()
        {
            // Aquí podrías realizar cualquier lógica necesaria para mostrar detalles adicionales del pedido, si es necesario
            return View();
        }
    }
}
