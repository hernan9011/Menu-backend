namespace Application.Interface.Command
{
    public interface IComandaCommand
    {
        void InsertComanda(Guid newComandaId, int id, int precioTotal);
        void RemoveComanda(int comandaId);
    }
}
