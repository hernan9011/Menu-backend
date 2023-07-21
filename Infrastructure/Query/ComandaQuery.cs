using Application.Interface.Query;
using Domain.Entity;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Query
{
    public class ComandaQuery: IComandaQuery
    {
        private readonly AppDbContext _context;
        public ComandaQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Comanda>> GetListComanda()
        {
            var comandas = await _context.Comanda
                .Include(s => s.FKFormaEntrega)
                .Include(s => s.LsComandaMercaderia)
                .ThenInclude(s => s.FKMercaderia)
                .ToListAsync();
            comandas = comandas.OrderBy(s => s.Fecha).ToList();
            return comandas;
        }

        public async Task<Comanda> GetComandaId(Guid comandaId)
        {
            var comandas = await _context.Comanda
                .Include(s => s.FKFormaEntrega)
                .Include(s => s.LsComandaMercaderia)
                .ThenInclude(s => s.FKMercaderia)
                .ThenInclude(s => s.FKTipoMercaderia)
                .FirstOrDefaultAsync(s => s.ComandaId == comandaId);
            return comandas;
        }
    }
}
