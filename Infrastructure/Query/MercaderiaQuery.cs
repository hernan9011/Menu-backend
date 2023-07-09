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
        public List<Mercaderia> GetListmercaderia()
        {
            var mercaderia = _context.Mercaderia.Include(s => s.FKTipoMercaderia).ToList();
            return mercaderia;
        }
        public List<Mercaderia> GetListmercaderia(string nombre)
        {
            var mercaderia = _context.Mercaderia.Where(s => s.Nombre.Contains(nombre)).Include(s => s.FKTipoMercaderia).ToList();
            return mercaderia;
        }

        public async Task<Mercaderia> GetSeach(string nombre)
        {
            var mercaderia = await _context.Mercaderia
                .Include(s => s.FKTipoMercaderia).FirstOrDefaultAsync(s => s.Nombre == nombre);
            return mercaderia;
        }

        public Mercaderia GetMercaderiaId(int mercaderiaId)
        {
             var mercaderia = _context.Mercaderia.Include(s => s.FKTipoMercaderia)
                .FirstOrDefault(s => s.MercaderiaId == mercaderiaId);
            return mercaderia;
        }

   
    }
}
