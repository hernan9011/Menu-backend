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
        public void InsertComandaMercaderia(Guid newComandaId, int id)
        {
             ComandaMercaderia comandaMercaderia = new()
             {
                ComandaId = newComandaId,
                MercaderiaId = id
            };
            _context.Add(comandaMercaderia);
            _context.SaveChanges();
        }

        public void RemoveComandaMercaderia(int comandaId)
        {
            throw new NotImplementedException();
        }
    }
}
