using Microsoft.AspNetCore.Mvc;
using XtremeZone.Data;
using XtremeZone.Models;
using System.Threading.Tasks;

namespace XtremeZone.Controllers
{
    public class ContactoController : Controller
    {
        private readonly XtremeZoneDBContext _context;

        public ContactoController(XtremeZoneDBContext context)
        {
            _context = context;
        }

        public IActionResult Contacto()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contacto(Contacto contacto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contacto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Confirmacion));
            }
            return View(contacto);
        }

        public IActionResult Confirmacion()
        {
            return View();
        }
    }
}
