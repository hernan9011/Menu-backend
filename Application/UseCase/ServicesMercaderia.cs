
using Application.Interface;
using Application.Responsive;
using AutoMapper;
using Domain.Entity;
using Application.Interface.Command;
using Application.Interface.Query;
using ServiceStack.Host;
using System.Runtime.ConstrainedExecution;

namespace Application.UseCase
{
    public class ServicesMercaderia : IServicesMercaderia
    {
        private readonly IMercaderiaCommand _command;
        private readonly IMercaderiaQuery _query;
        private readonly IMapper _mapper;
        private readonly IComandaMercaderiaQuery _querycom;

        public ServicesMercaderia(IMercaderiaCommand command, IMercaderiaQuery query,
            IMapper mapper, IComandaMercaderiaQuery querycom)
        {
            _command = command;
            _query = query;
            _mapper = mapper;
            _querycom = querycom;

        }
        public async Task<List<MercaderiaGetResponse>> GetBuscar(int? tipo, string? nombre, string? orden)
        {
            orden ??= "asc";
            List<Mercaderia> mercaderias;
            List<Mercaderia> mer;
            if (string.IsNullOrEmpty(nombre))
            {
                mer = _query.GetListmercaderia();
            }
            else
            {
                mer = _query.GetListmercaderia(nombre.ToUpper());
            }
            if (tipo != null)
            {
                mercaderias = orden.ToLower() == "desc" ? mer.FindAll(s => s.TipoMercaderiaId == tipo)
                 .OrderByDescending(x => x.Precio).ToList() : mer.FindAll(s => s.TipoMercaderiaId == tipo).OrderBy(x => x.Precio).ToList();
            }
            else
            {
                mercaderias = orden.ToLower() == "desc" ? mer.OrderByDescending(x => x.Precio).ToList() : mer
                        .OrderBy(x => x.Precio).ToList();
            }
            return _mapper.Map<List<MercaderiaGetResponse>>(mercaderias);
        }
        public Task<MercaderiaResponse> InsertMer(MercaderiaRequest request)
        {
            var mercaderia = new Mercaderia
            {
                Nombre = request.Nombre.ToUpper(),
                TipoMercaderiaId = request.Tipo,
                Precio = (int)request.Precio,
                Ingredientes = request.Ingredientes,
                Preparacion = request.Preparacion,
                Imagen = request.Imagen,
            };
            _command.InsertMercaderia(mercaderia);
            var item = _query.GetListmercaderia().First(s => s.Nombre == request.Nombre.ToUpper());
            var mer = _mapper.Map<MercaderiaResponse>(item);
            return Task.FromResult(mer);
        }

        public Task<MercaderiaResponse> GetMerId(int mercaderiaId)
        {
            var mercaderia = _query.GetMercaderiaId(mercaderiaId);
            var mer = _mapper.Map<MercaderiaResponse>(mercaderia);
            return Task.FromResult(mer);
        }

        public Task<MercaderiaResponse> PutMerId(int Id, MercaderiaRequest request)
        {
            var mercaderia = _query.GetMercaderiaId(Id);
            mercaderia.Nombre = request.Nombre;
            mercaderia.TipoMercaderiaId = request.Tipo;
            mercaderia.Precio = (int)request.Precio;
            mercaderia.Ingredientes = request.Ingredientes;
            mercaderia.Preparacion = request.Preparacion;
            mercaderia.Imagen = request.Imagen;
            _command.UpdateMercaderia(mercaderia);
            var mer = _mapper.Map<MercaderiaResponse>(mercaderia);
            return Task.FromResult(mer);
        }
        public Task<MercaderiaResponse> RemoveMer(int Id)
        {
            var mercaderia = _query.GetMercaderiaId(Id);
            var condicion = _querycom.GetListComandaMercaderia().FirstOrDefault(s => s.MercaderiaId == Id);

            if (mercaderia != null && condicion == null)
            {
                var mer = _mapper.Map<MercaderiaResponse>(mercaderia);
                _command.RemoveMercaderia(mercaderia);
                return Task.FromResult(mer);
            }
            else
            {
                return Task.FromResult(new MercaderiaResponse());
            }
        }

        public async Task<Mercaderia> Search(string nombre)
        {
            var mer = await _query.GetSeach(nombre.ToUpper());
            
            return mer;
        }
    }
}
   