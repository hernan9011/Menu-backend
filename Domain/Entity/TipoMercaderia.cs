namespace Domain.Entity
{
    public class TipoMercaderia
    {
        public int TipoMercaderiaId { get; set; }
        public string Descripcion { get; set; }
        public virtual List<Mercaderia> LsMercaderia { get; set; }
    }
}
