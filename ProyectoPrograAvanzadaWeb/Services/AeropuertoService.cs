using Microsoft.EntityFrameworkCore;
using ProyectoPrograAvanzadaWeb.Models;

namespace ProyectoPrograAvanzadaWeb.Services
{
    public class AeropuertoService : IAeropuertoService
    {
        private readonly PrograContext _context;

        public AeropuertoService(PrograContext context)
        {
            _context = context;
        }
        public async Task<List<Aeropuerto>> GetAeropuertos()
        {
            return await _context.Aeropuertos.ToListAsync();
        }
        public async Task<Aeropuerto?> GetAeropuerto(int id)
        {
            return await _context.Aeropuertos.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Aeropuerto> CreateAeropuerto(Aeropuerto aeropuerto)
        {
            _context.Aeropuertos.Add(aeropuerto);
            await _context.SaveChangesAsync();
            return aeropuerto;
        }
        public async Task<Aeropuerto> UpdateAeropuerto(Aeropuerto aeropuerto)
        {
            _context.Entry(aeropuerto).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return aeropuerto;
        }
        public async Task<Aeropuerto?> DeleteAeropuerto(int id)
        {

            var aeropuerto = await _context.Aeropuertos.FirstOrDefaultAsync(x => x.Id == id);
            if (aeropuerto == null)
            {
                return null;
            }
            _context.Aeropuertos.Remove(aeropuerto);
            await _context.SaveChangesAsync();
            return aeropuerto;
        }

    }

}
