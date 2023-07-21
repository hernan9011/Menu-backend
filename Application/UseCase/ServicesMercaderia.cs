
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
            List<Mercaderia> mer = string.IsNullOrEmpty(nombre)
                ? await _query.GetListmercaderia()
                : await _query.GetListmercaderia(nombre.ToUpper());

            mercaderias = tipo != null
            ? (orden.ToLower() == "desc" 
                    ? mer.Where(s => s.TipoMercaderiaId == tipo).OrderByDescending(x => x.Precio).ToList()
                    : mer.Where(s => s.TipoMercaderiaId == tipo).OrderBy(x => x.Precio).ToList())
            
            : (orden.ToLower() == "desc" 
                    ? mer.OrderByDescending(x => x.Precio).ToList()
                    : mer.OrderBy(x => x.Precio).ToList());

            return _mapper.Map<List<MercaderiaGetResponse>>(mercaderias);
        }

        public async Task<MercaderiaResponse> InsertMer(MercaderiaRequest request)
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
            var List = await _query.GetListmercaderia();
            var result = _mapper.Map<MercaderiaResponse>(List.FirstOrDefault(s => s.Nombre == request.Nombre.ToUpper()));
            return result;
        }

        public async Task<MercaderiaResponse> GetMerId(int mercaderiaId)
        {
            var mercaderia =await _query.GetMercaderiaId(mercaderiaId);
            var result = _mapper.Map<MercaderiaResponse>(mercaderia);
            return result;
        }

        public async Task<MercaderiaResponse> PutMerId(int Id, MercaderiaRequest request)
        {
            var mercaderia =await _query.GetMercaderiaId(Id);
            mercaderia.Nombre = request.Nombre;
            mercaderia.TipoMercaderiaId = request.Tipo;
            mercaderia.Precio = (int)request.Precio;
            mercaderia.Ingredientes = request.Ingredientes;
            mercaderia.Preparacion = request.Preparacion;
            mercaderia.Imagen = request.Imagen;
            _command.UpdateMercaderia(mercaderia);
            var mer = _mapper.Map<MercaderiaResponse>(mercaderia);
            return mer;
        }
        public async Task<MercaderiaResponse> RemoveMer(int Id)
        {
            var mercaderia =await _query.GetMercaderiaId(Id);
            var condicion = _querycom.GetListComandaMercaderia().FirstOrDefault(s => s.MercaderiaId == Id);

            if (mercaderia != null && condicion == null)
            {
                var result = _mapper.Map<MercaderiaResponse>(mercaderia);
                _command.RemoveMercaderia(mercaderia);
                return result;
            }
            else
            {
                return new MercaderiaResponse();
            }
        }

        public async Task<Mercaderia> Search(string nombre)
        {
            var result = await _query.GetSeach(nombre.ToUpper());
            return result;
        }
    }
}
   