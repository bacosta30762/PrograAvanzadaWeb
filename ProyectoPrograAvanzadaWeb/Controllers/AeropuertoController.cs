using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

            try
            {
                await _aeropuertoService.UpdateAeropuerto(aeropuerto);

                // Asignar mensaje de éxito
                TempData["SuccessMessage"] = "El aeropuerto se ha actualizado con éxito.";

                return RedirectToAction("Edit", new { id = aeropuerto.Id }); // Redirigir a la misma vista
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(aeropuerto);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Ocurrió un error inesperado.");
                return View(aeropuerto);
            }
        }


        public async Task<IActionResult> Delete(int id)
        {
            await _aeropuertoService.DeleteAeropuerto(id);
            TempData["SuccessMessage"] = "El aeropuerto se ha eliminado con éxito.";
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Details(int id)
        {
            var aeropuerto = await _aeropuertoService.GetAeropuerto(id);
            if (aeropuerto == null)
            {
                return NotFound();
            }
            return View(aeropuerto);
        }

    }
}
