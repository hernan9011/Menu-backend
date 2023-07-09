using Domain.Entity;

namespace Application.Interface.Query
{
    public interface IMercaderiaQuery
    {
        List<Mercaderia> GetListmercaderia();
        Mercaderia GetMercaderiaId(int mercaderiaId);
        List<Mercaderia> GetListmercaderia(string nombre);
        Task<Mercaderia> GetSeach(string nombre);
    }
}
