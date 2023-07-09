using Application.Responsive;
using Domain.Entity;

namespace Application.Interface.Command
{
    public interface IMercaderiaCommand
    {
        void InsertMercaderia(Mercaderia mercaderia);
        void RemoveMercaderia(Mercaderia mercaderia);
        void UpdateMercaderia(Mercaderia mercaderia);
    }
}
