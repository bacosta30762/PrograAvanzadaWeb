using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoPrograAvanzadaWeb.Models;
using ProyectoPrograAvanzadaWeb.Services;

namespace ProyectoPrograAvanzadaWeb.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AeropuertoController : Controller
    {
        private readonly IAeropuertoService _aeropuertoService;

        public AeropuertoController(IAeropuertoService aeropuertoService)
        {
            _aeropuertoService = aeropuertoService;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var aeropuertos = await _aeropuertoService.GetAeropuertos();
            return View(aeropuertos);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(Aeropuerto aeropuerto)
        {
            if (!ModelState.IsValid)
            {
                return View(aeropuerto);
            }
            await _aeropuertoService.CreateAeropuerto(aeropuerto);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int id)
        {
            var aeropuerto = await _aeropuertoService.GetAeropuerto(id);
            if (aeropuerto == null)
            {
                return NotFound();
            }
            return View(aeropuerto);
        }
        [HttpPost]
        public async Task<IActionResult> EditAsync(Aeropuerto aeropuerto)
        {
            if (!ModelState.IsValid)
            {
                return View(aeropuerto);
            }
            await _aeropuertoService.UpdateAeropuerto(aeropuerto);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _aeropuertoService.DeleteAeropuerto(id);
            return RedirectToAction("Index");
        }
        
    }
}
