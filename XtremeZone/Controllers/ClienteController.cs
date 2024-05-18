using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using XtremeZone.Data;
using XtremeZone.Models;

namespace XtremeZone.Controllers
{
    public class ClienteController : Controller
    {
        private readonly XtremeZoneDBContext _context;

        public ClienteController(XtremeZoneDBContext context)
        {
            _context = context;
        }
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear([Bind("Nombre,Apellido,Edad,Correo,Contrasenia")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                cliente.FechaCreacion = DateTime.Now;
                _context.Add(cliente);
                _context.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(cliente);
        }

        public IActionResult Login()
        {
            return View();
        }


        // POST: /Cliente/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string correo, string contrasenia)
        {
            var cliente = _context.Clientes.SingleOrDefault(c => c.Correo == correo && c.Contrasenia == contrasenia);
            if (cliente != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, cliente.Nombre),
                    new Claim(ClaimTypes.Email, cliente.Correo)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties { };

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties).Wait();

                return RedirectToAction("Index","Producto");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Correo o contraseña incorrectos");
                return View();
            }
        }
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();
            return RedirectToAction("Login");
        }
    }
}
