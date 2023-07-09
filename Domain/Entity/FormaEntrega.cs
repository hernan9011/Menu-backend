namespace Domain.Entity
{
    public class FormaEntrega
    {
        public int FormaEntregaId { get; set; }
        public string Descripcion { get; set; }
        public List<Comanda> LsComanda { get; set; }

    }
}
