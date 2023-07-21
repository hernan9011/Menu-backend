using Application.Interface.Query;
using Domain.Entity;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Query
{
    public class MercaderiaQuery : IMercaderiaQuery
    {
        private readonly AppDbContext _context;
        public MercaderiaQuery(AppDbContext context) 
        { 
           _context = context;
        }
        public async Task<List<Mercaderia>> GetListmercaderia()
        {
            var mercaderia = await _context.Mercaderia
                .Include(s => s.FKTipoMercaderia).ToListAsync();
            return mercaderia;
        }
        public async Task<List<Mercaderia>> GetListmercaderia(string nombre)
        {
            var mercaderia = await _context.Mercaderia
                .Where(s => s.Nombre.Contains(nombre))
                .Include(s => s.FKTipoMercaderia).ToListAsync();
            return mercaderia;
        }

        public async Task<Mercaderia> GetSeach(string nombre)
        {
            var mercaderia = await _context.Mercaderia
                .Include(s => s.FKTipoMercaderia)
                .FirstOrDefaultAsync(s => s.Nombre == nombre);
            return mercaderia;
        }

        public async Task<Mercaderia> GetMercaderiaId(int mercaderiaId)
        {
             var mercaderia = await _context.Mercaderia.Include(s => s.FKTipoMercaderia)
                .FirstOrDefaultAsync(s => s.MercaderiaId == mercaderiaId);
            return mercaderia;
        }
    }
}
