using AutoMapper;
using Financeiro.Application.DTOs.Usuario;
using Financeiro.Domain.Entities;

namespace Financeiro.Application.Mappings;

public class UsuarioMapping : Profile
{
    public UsuarioMapping()
    {
        CreateMap<Usuario, UsuarioDto>()
            .ForMember(u => u.UsuarioId, opt => opt.MapFrom(us => us.UsuarioId))
            .ForMember(u => u.Nome, opt => opt.MapFrom(us => us.Nome))
            .ForMember(u => u.Email, opt => opt.MapFrom(us => us.Email))
            .ForMember(u => u.Status, opt => opt.MapFrom(us => us.Status.ToString()))
            .ForMember(u => u.DataCadastro, opt => opt.MapFrom(us => us.DataCadastro))
            .ForMember(u => u.DataUltimaAtualizacao, opt => opt.MapFrom(us => us.DataUltimaAtualizacao));

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