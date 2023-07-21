namespace Application.Interface.Command
{
    public interface IComandaCommand
    {
        Task InsertComanda(Guid newComandaId, int id, int precioTotal);
        void RemoveComanda(int comandaId);
    }
}
