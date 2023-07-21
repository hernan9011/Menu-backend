using Domain.Entity;

namespace Application.Interface.Query
{
    public interface IComandaQuery
    {
        Task<List<Comanda>> GetListComanda();
        Task<Comanda> GetComandaId(Guid comandaId);
    }
}
