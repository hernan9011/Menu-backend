using Domain.Entity;

namespace Application.Interface.Query
{
    public interface IComandaMercaderiaQuery
    {
        ComandaMercaderia GetComandaMercaderiaId(int comandamercaderiaId);
        List<ComandaMercaderia> GetListComandaMercaderia();
    }
}
