using ProyectoPrograAvanzadaWeb.Models;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public class FlightService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public FlightService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<FlightResponse> GetFlightsAsync(string origen, string destino, string aerolinea, string estado)
    {
        var baseUrl = _configuration["AviationStack:BaseUrl"];
        var apiKey = _configuration["AviationStack:ApiKey"];

        var url = $"{baseUrl}/flights?access_key={apiKey}";

        if (!string.IsNullOrEmpty(origen))
            url += $"&dep_iata={origen}";
        if (!string.IsNullOrEmpty(destino))
            url += $"&arr_iata={destino}";
        if (!string.IsNullOrEmpty(aerolinea))
            url += $"&airline_name={aerolinea}";
        if (!string.IsNullOrEmpty(estado))
            url += $"&flight_status={estado}";

        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
            throw new Exception($"Error al consumir la API: {response.StatusCode}");

        var content = await response.Content.ReadAsStringAsync();

        var flightResponse = JsonSerializer.Deserialize<FlightResponse>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return flightResponse;
    }
}
