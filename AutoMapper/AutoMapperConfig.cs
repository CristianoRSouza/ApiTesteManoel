using AutoMapper;
using LojaManoelApi.Data.Dtos;
using LojaManoelApi.Data.Entities;

namespace LojaManoelApi.AutoMapper
{
    public class AutoMapperConfig:Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<PedidoDto, Pedido>().ReverseMap();
            CreateMap<ProdutoDto, Produto>().ReverseMap();
            CreateMap<DimensaoDto, Dimensao>().ReverseMap();
            CreateMap<CaixaDto, Caixa>().ReverseMap();
            CreateMap<UsuarioDto, Usuario>().ReverseMap();
            CreateMap<PapelDto, Papel>().ReverseMap();

            CreateMap<Usuario, UsuarioDto>()
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.PasswordHash));
        }
    }
}
