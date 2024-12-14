using Microsoft.AspNetCore.Mvc;
using ProyectoPrograAvanzadaWeb.Services;
using System.Threading.Tasks;

public class VuelosController : Controller
{
    private readonly FlightService _flightService;
    private readonly IAeropuertoService _aeropuertoService;

    public VuelosController(FlightService flightService, IAeropuertoService aeropuertoService)
    {
        _flightService = flightService;
        _aeropuertoService = aeropuertoService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string origen, string destino, string aerolinea, string estado)
    {
        var response = await _flightService.GetFlightsAsync(origen, destino, aerolinea, estado);
        var aeropuertos = await _aeropuertoService.GetAeropuertos();
        ViewData["Pagination"] = response.Pagination;
        ViewData["Aeropuertos"] = aeropuertos;
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
