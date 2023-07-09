
namespace Application.Responsive
{
    public class ComandaGetResponse
    {
        public Guid Id { get; set; }
        public List<MercaderiaGetResponse> Mercaderias { get; set; }
        public FormaEntregas FormaEntrega { get; set; }
        public double Total { get; set; }
        public DateTime Fecha { get; set; }
    }
}
