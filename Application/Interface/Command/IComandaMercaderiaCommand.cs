namespace Application.Interface.Command
{
    public interface IComandaMercaderiaCommand
    {
        Task InsertComandaMercaderia(Guid newComandaId, int id);
        void RemoveComandaMercaderia(int comandaId);
    }
}
