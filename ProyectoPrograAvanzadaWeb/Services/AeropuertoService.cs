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
            // Buscar la entidad existente en la base de datos
            var existingAeropuerto = await _context.Aeropuertos.FirstOrDefaultAsync(x => x.Id == aeropuerto.Id);
            if (existingAeropuerto == null)
            {
                throw new InvalidOperationException("El aeropuerto no existe en la base de datos.");
            }

            // Actualizar propiedades manualmente
            existingAeropuerto.IATA = aeropuerto.IATA;
            existingAeropuerto.Nombre = aeropuerto.Nombre;
            existingAeropuerto.Ciudad = aeropuerto.Ciudad;
            existingAeropuerto.Pais = aeropuerto.Pais;

            // Guardar cambios
            await _context.SaveChangesAsync();
            return existingAeropuerto;
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
