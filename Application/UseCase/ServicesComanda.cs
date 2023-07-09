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
        public Task<List<ComandaResponse>> GetSearchId(string? fecha)
        {
            if (!fecha.IsNullOrEmpty())
            {
                DateTime datafecha = DateTime.Parse(fecha);
                var mer = _query.GetListComanda().FindAll(s => s.Fecha == datafecha).ToList();
                var conber = _mapper.Map<List<ComandaResponse>>(mer);
                return Task.FromResult(conber);
            }
            else
            {
                var mer = _query.GetListComanda().ToList();
                var conber = _mapper.Map<List<ComandaResponse>>(mer);
                return Task.FromResult(conber);
            }     
        }

        public Task<ComandaGetResponse> GetSearchId(Guid Id)
        {
            var item = _query.GetComandaId(Id);
            var com = _mapper.Map<ComandaGetResponse>(item);
            return Task.FromResult(com);
        }
        public Task<ComandaResponse> InsertCom(ComandaRequest request)
        {   
            Guid newComandaId = Guid.NewGuid();
            int PrecioTotal = 0;
            List<Mercaderia> Pedido = new();

            foreach (var num in request.Mercaderias)
            {
                Pedido.Add(_mercaderia.GetMercaderiaId(num));
                PrecioTotal += _mercaderia.GetMercaderiaId(num).Precio;
            }
            _command.InsertComanda( newComandaId , request.FormaEntrega, PrecioTotal);
            for (int i = 0; i < Pedido.Count; i++)
            {
                _querycomMer.InsertComandaMercaderia(newComandaId, Pedido[i].MercaderiaId);
            }
            var item = _query.GetComandaId(newComandaId);
            var com = _mapper.Map<ComandaResponse>(item);
            return Task.FromResult(com);
        }

    }
}
