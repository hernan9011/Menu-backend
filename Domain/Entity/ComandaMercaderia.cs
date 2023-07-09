namespace Domain.Entity
{
    public class ComandaMercaderia
    {
        public int ComandaMercaderiaId { get; set; }
        public int MercaderiaId { get; set; }
        public Guid ComandaId { get; set; }
        public virtual Comanda FKComanda { get; set; }
        public virtual Mercaderia FKMercaderia { get; set; }

    }
}
