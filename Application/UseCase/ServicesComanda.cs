using Application.Interface;
using Application.Interface.Command;
using Application.Interface.Query;
using Application.Responsive;
using AutoMapper;
using Domain.Entity;
using ServiceStack;
using ServiceStack.Host;

namespace Application.UseCase
{
    public class ServicesComanda : IServicesComanda
    {
        private readonly IComandaCommand _command;
        private readonly IComandaQuery _query;
        private readonly IMapper _mapper;
        public readonly IMercaderiaQuery _mercaderia;
        public readonly IComandaMercaderiaCommand _querycomMer;

        public ServicesComanda(IComandaCommand command, IComandaQuery query,
            IMapper mapper, IMercaderiaQuery mercaderia, IComandaMercaderiaCommand querycomMer)
        {
            _command = command;
            _query = query;

            _mapper = mapper;
            _mercaderia = mercaderia;
            _querycomMer = querycomMer;
        }
 
        public async Task<List<ComandaResponse>> GetSearchId(string? fecha)
        {
            var comandas = await _query.GetListComanda();
            if (!fecha.IsNullOrEmpty())
            {
                comandas = comandas.Where(s => s.Fecha == DateTime.Parse(fecha)).ToList();
            }
            var conber = _mapper.Map<List<ComandaResponse>>(comandas);
            return conber;
        }

        public async Task<ComandaGetResponse> GetSearchId(Guid Id)
        {
            var item = await _query.GetComandaId(Id);
            var com = _mapper.Map<ComandaGetResponse>(item);
            return com;
        }
        
        public async Task<ComandaResponse> InsertCom(ComandaRequest request)
        {   
            Guid newComandaId = Guid.NewGuid();
            int PrecioTotal = 0;
            List<Mercaderia> Pedido = new();

            foreach (var num in request.Mercaderias)
            {
                Pedido.Add( await _mercaderia.GetMercaderiaId(num));
                var mercaderia = await _mercaderia.GetMercaderiaId(num);
                PrecioTotal += mercaderia.Precio;
            }
            await _command.InsertComanda( newComandaId , request.FormaEntrega, PrecioTotal);
            for (int i = 0; i < Pedido.Count; i++)
            {
              await  _querycomMer.InsertComandaMercaderia(newComandaId, Pedido[i].MercaderiaId);
            }
            var item = await _query.GetComandaId(newComandaId);
            var com = _mapper.Map<ComandaResponse>(item);
            return com;
        }

    }
}
