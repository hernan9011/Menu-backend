using Application.Interface.Command;
using Domain.Entity;
using Infrastructure.Persistence;

namespace Infrastructure.Command
{
    public class ComandaMercaderiaCommand : IComandaMercaderiaCommand 
    {
        private readonly AppDbContext _context;
        public ComandaMercaderiaCommand(AppDbContext context)
        {
            _context = context;
        }   
        public async Task InsertComandaMercaderia(Guid newComandaId, int id)
        {
             ComandaMercaderia comandaMercaderia = new()
             {
                ComandaId = newComandaId,
                MercaderiaId = id
            };
            _context.ComandaMercaderia.Add(comandaMercaderia);
            await _context.SaveChangesAsync();
        }

        public void RemoveComandaMercaderia(int comandaId)
        {
            throw new NotImplementedException();
        }
    }
}
