using Application.Interface.Query;
using Domain.Entity;
using Infrastructure.Persistence;

namespace Infrastructure.Query
{
    public class ComandaMercaderiaQuery : IComandaMercaderiaQuery
    {
        private readonly AppDbContext _context;
        public ComandaMercaderiaQuery(AppDbContext context)           
        { 
            _context = context;
        }
        public ComandaMercaderia GetComandaMercaderiaId(int comandamercaderiaId)
        {
            throw new NotImplementedException();
        }

        public List<ComandaMercaderia> GetListComandaMercaderia()
        {
             var comandasMercaderias = _context.ComandaMercaderia.ToList();
             return comandasMercaderias;   
        }
    }
}
