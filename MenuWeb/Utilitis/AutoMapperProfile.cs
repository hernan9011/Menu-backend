using AutoMapper;
using Domain.Entity;
using Application.Responsive;

namespace MenuWeb.Utilitis
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region TipoMercaderia
            CreateMap<TipoMercaderia, TipoMercaderiaResponse>()
            .ForMember(d => d.Id, o => o.MapFrom(o => o.TipoMercaderiaId));
            #endregion

            #region Mercaderia
            CreateMap<Mercaderia, MercaderiaResponse>()
            .ForMember(d => d.Id, o => o.MapFrom(o => o.MercaderiaId))
            .ForMember(d => d.Tipo, o => o.MapFrom(o => o.FKTipoMercaderia));
            #endregion  
            
            #region Mercaderia
            CreateMap<Mercaderia, MercaderiaGetResponse>()
            .ForMember(d => d.Id, o => o.MapFrom(o => o.MercaderiaId))
            .ForMember(d => d.Tipo, o => o.MapFrom(o => o.FKTipoMercaderia));
            #endregion

            #region Comanda
            CreateMap<Comanda, ComandaResponse>()
            .ForMember(d => d.Id, o => o.MapFrom(o => o.ComandaId))
            .ForMember(d => d.Mercaderias, o => o.MapFrom(o => o.LsComandaMercaderia))
            .ForMember(d => d.FormaEntrega, o => o.MapFrom(o => o.FKFormaEntrega))
            .ForMember(d => d.Total, o => o.MapFrom(o => o.PrecioTotal))
            .ForMember(d => d.Fecha, o => o.MapFrom(o => o.Fecha));
            #endregion
            
            #region Comanada
            CreateMap<ComandaMercaderia, MercaderiaComandaResponse>()
            .ForMember(d => d.Tipo, o => o.MapFrom(o => o.FKMercaderia.MercaderiaId))
            .ForMember(d => d.Nombre, o => o.MapFrom(o => o.FKMercaderia.Nombre))
            .ForMember(d => d.Precio, o => o.MapFrom(o => o.FKMercaderia.Precio));
            #endregion   
            
            #region FormaEntrega
            CreateMap<FormaEntrega, FormaEntregas>()
            .ForMember(d => d.Id, o => o.MapFrom(o => o.FormaEntregaId))
            .ForMember(d => d.Descripcion, o => o.MapFrom(o => o.Descripcion));
            #endregion

            #region Comandaget
            CreateMap<Comanda, ComandaGetResponse>()
            .ForMember(d => d.Id, o => o.MapFrom(o => o.ComandaId))
            .ForMember(d => d.Mercaderias, o => o.MapFrom(o => o.LsComandaMercaderia))
            .ForMember(d => d.FormaEntrega, o => o.MapFrom(o => o.FKFormaEntrega))
            .ForMember(d => d.Total, o => o.MapFrom(o => o.PrecioTotal))
            .ForMember(d => d.Fecha, o => o.MapFrom(o => o.Fecha));
            #endregion

            #region Comanadaget
            CreateMap<ComandaMercaderia, MercaderiaGetResponse>()
            .ForMember(d => d.Id, o => o.MapFrom(o => o.FKMercaderia.MercaderiaId))
            .ForMember(d => d.Nombre, o => o.MapFrom(o => o.FKMercaderia.Nombre))
            .ForMember(d => d.Precio, o => o.MapFrom(o => o.FKMercaderia.Precio))
            .ForMember(d => d.Tipo, o => o.MapFrom(o => o.FKMercaderia.FKTipoMercaderia))
            .ForMember(d => d.Imagen, o => o.MapFrom(o => o.FKMercaderia.Imagen));
            #endregion

        }
    }
}
