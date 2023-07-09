namespace Domain.Entity
{
    public class Comanda
    {
        public Guid ComandaId { get; set; }
        public int FormaEntregaId { get; set; }
        public int PrecioTotal { get; set; }
        public DateTime Fecha { get; set; }
        public virtual FormaEntrega FKFormaEntrega { get; set; }
        public virtual List<ComandaMercaderia> LsComandaMercaderia { get; set; }
    }
}
