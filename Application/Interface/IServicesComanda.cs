using Application.Responsive;
using Domain.Entity;

namespace Application.Interface
{
    public interface IServicesComanda
    {
        Task<List<ComandaResponse>> GetSearchId(string? fecha);
        Task<ComandaGetResponse> GetSearchId(Guid Id);
        Task<ComandaResponse> InsertCom(ComandaRequest request);
    }
}
