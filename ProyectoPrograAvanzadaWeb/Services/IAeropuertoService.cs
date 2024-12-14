using ProyectoPrograAvanzadaWeb.Models;

namespace ProyectoPrograAvanzadaWeb.Services
{
    public interface IAeropuertoService
    {
        Task<Aeropuerto> CreateAeropuerto(Aeropuerto aeropuerto);
        Task<Aeropuerto?> DeleteAeropuerto(int id);
        Task<Aeropuerto?> GetAeropuerto(int id);
        Task<List<Aeropuerto>> GetAeropuertos();
        Task<Aeropuerto> UpdateAeropuerto(Aeropuerto aeropuerto);
    }
}