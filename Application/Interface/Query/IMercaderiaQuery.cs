using Domain.Entity;

namespace Application.Interface.Query
{
    public interface IMercaderiaQuery
    {
        Task<List<Mercaderia>> GetListmercaderia();
        Task<Mercaderia> GetMercaderiaId(int mercaderiaId);
        Task<List<Mercaderia>> GetListmercaderia(string nombre);
        Task<Mercaderia> GetSeach(string nombre);
    }
}
