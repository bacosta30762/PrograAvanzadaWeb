using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

public class VuelosController : Controller
{
    private readonly FlightService _flightService;

    public VuelosController(FlightService flightService)
    {
        _flightService = flightService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string origen, string destino, string aerolinea, string estado)
    {
        var response = await _flightService.GetFlightsAsync(origen, destino, aerolinea, estado);

        ViewData["Pagination"] = response.Pagination;
        return View(response.Data);
    }

    [HttpGet]
    public async Task<IActionResult> Detalle(string numeroVuelo)
    {
        var response = await _flightService.GetFlightsAsync(null, null, null, null);

        // Buscar el vuelo específico por número
        var vuelo = response.Data.FirstOrDefault(v => v.Flight.Number == numeroVuelo);

        if (vuelo == null)
        {
            return NotFound("Vuelo no encontrado.");
        }

        return View(vuelo);
    }

}
