using AutoMapper;
using Financeiro.Application.DTOs.Usuario;
using Financeiro.Domain.Entities;

namespace Financeiro.Application.Mappings;

public class UsuarioMapping : Profile
{
    public UsuarioMapping()
    {
        CreateMap<Usuario, UsuarioDto>()
            .ForMember(u => u.UsuarioId, opt => opt.Ignore())
            .ForMember(u => u.Nome, opt => opt.Ignore())
            .ForMember(u => u.Email, opt => opt.Ignore())
            .ForMember(u => u.DataCadastro, opt => opt.Ignore())
            .ForMember(u => u.Status, opt => opt.MapFrom(us => us.Status.ToString()));

        CreateMap<UsuarioCadastroDto, Usuario>()
            .ForMember(u => u.Email, opt => opt.Ignore())
            .ForMember(u => u.Nome, opt => opt.Ignore())
            .ForMember(u => u.Senha, opt => opt.Ignore());


        CreateMap<UsuarioAtualizacaoDto, Usuario>()
            .ForMember(u => u.UsuarioId, opt => opt.Ignore())
            .ForMember(u => u.Email, opt => opt.Ignore())
            .ForMember(u => u.Nome, opt => opt.Ignore())
            .ForMember(u => u.Senha, opt => opt.Ignore());
    }
}