using Application.Responsive;
using Domain.Entity;

namespace Application.Interface
{
    public interface IServicesMercaderia
    {   
        Task<List<MercaderiaGetResponse>> GetBuscar(int? tipo, string? nombre, string? orden);
        Task<MercaderiaResponse> GetMerId(int mercaderiaId);
        Task<MercaderiaResponse> InsertMer(MercaderiaRequest request);      
        Task<MercaderiaResponse> PutMerId(int Id, MercaderiaRequest request);
        Task<MercaderiaResponse> RemoveMer(int Id);
        Task<Mercaderia> Search(string nombre);
    }
}
