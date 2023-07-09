using Domain.Entity;

namespace Application.Interface.Query
{
    public interface IComandaQuery
    {
        List<Comanda> GetListComanda();
        Comanda GetComandaId(Guid comandaId);
    }
}
