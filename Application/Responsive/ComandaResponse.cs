
namespace Application.Responsive
{
    public class ComandaResponse
    {
        public Guid Id { get; set; }
        public List<MercaderiaComandaResponse> Mercaderias { get; set; }      
        public  FormaEntregas FormaEntrega { get; set; }
        public double Total { get; set; }
        public DateTime Fecha { get; set; } 
    }
}
