using Application.Interface.Command;
using Domain.Entity;
using Infrastructure.Persistence;

namespace Infrastructure.Command
{
    public class ComandaCommand :  IComandaCommand
    {
        private readonly AppDbContext _context;
        public ComandaCommand(AppDbContext context)
        {
            _context = context;
        }
        public void InsertComanda(Guid newComandaId, int id, int precioTotal)
        {
              Comanda comanda = new()
              {
                ComandaId = newComandaId,
                FormaEntregaId = id,
                PrecioTotal = precioTotal,
                Fecha = DateTime.Now
            };
            _context.Add(comanda);
            _context.SaveChanges();
        }

        public void RemoveComanda(int comandaId)
        {
            throw new NotImplementedException();
        }
    }
}
