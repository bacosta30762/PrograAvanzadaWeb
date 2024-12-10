using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

namespace ProyectoPrograAvanzadaWeb.Controllers
{
    public class AlertasController : Controller
    {
        private readonly FlightService _flightService;

        public AlertasController(FlightService flightService)
        {
            _flightService = flightService;
        }


        [HttpGet]
        public async Task<IActionResult> AlertasJson(string origen, string destino, string aerolinea, string estado)
        {
            var response = await _flightService.GetFlightsAsync(origen, destino, aerolinea, estado);
            return Json(response.Data);
        }







    }
}
