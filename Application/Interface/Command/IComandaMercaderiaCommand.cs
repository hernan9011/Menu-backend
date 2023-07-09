namespace Application.Interface.Command
{
    public interface IComandaMercaderiaCommand
    {
        void InsertComandaMercaderia(Guid newComandaId, int id);
        void RemoveComandaMercaderia(int comandaId);
    }
}
