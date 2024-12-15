using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProyectoPrograAvanzadaWeb.Services;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Kernel.Font;
using System.Globalization;

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

    [HttpGet]
    public async Task<IActionResult> DescargarPDF(string origen, string destino, string aerolinea, string estado)
    {
        // Obtener datos de vuelos
        var response = await _flightService.GetFlightsAsync(origen, destino, aerolinea, estado);
        var vuelos = response.Data;

        // Crear PDF en memoria
        using var memoryStream = new MemoryStream();
        var writer = new PdfWriter(memoryStream);
        var pdf = new PdfDocument(writer);
        var document = new Document(pdf);

        // Fuente y texto en negrita
        var font = PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.HELVETICA_BOLD);
        var boldText = new Text("Consulta de Vuelos").SetFont(font);
        document.Add(new Paragraph(boldText).SetFontSize(18));

        // Fecha de generación
        document.Add(new Paragraph($"Fecha de generación: {DateTime.Now:yyyy-MM-dd HH:mm:ss}"));

        // Crear tabla
        var table = new iText.Layout.Element.Table(UnitValue.CreatePercentArray(5)).UseAllAvailableWidth();
        table.AddHeaderCell("Origen");
        table.AddHeaderCell("Destino");
        table.AddHeaderCell("Aerolínea");
        table.AddHeaderCell("Estado");
        table.AddHeaderCell("Horario de Salida");

        // Agregar datos de vuelos a la tabla
        foreach (var vuelo in vuelos)
        {
            table.AddCell(vuelo.Departure.Iata ?? "No disponible");
            table.AddCell(vuelo.Arrival.Iata ?? "No disponible");
            table.AddCell(vuelo.Airline.Name ?? "No disponible");
            table.AddCell(vuelo.FlightStatus ?? "No disponible");
            table.AddCell(vuelo.Departure.Scheduled?.ToString() ?? "No disponible");
        }

        document.Add(table);
        document.Close();

        // Devolver el archivo PDF
        return File(memoryStream.ToArray(), "application/pdf", "ConsultaVuelos.pdf");
    }

}
