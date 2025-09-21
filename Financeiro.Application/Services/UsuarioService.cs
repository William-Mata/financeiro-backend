using AutoMapper;
using Financeiro.Application.DTOs.Usuario;
using Financeiro.Application.Services.Interfaces;
using Financeiro.Domain.Entities;
using Financeiro.Domain.Interfaces;

namespace Financeiro.Application.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UsuarioService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UsuarioDto>> ListarAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var usuarios = await _unitOfWork.UsuarioRepository.ListarAsync(cancellationToken);

            return _mapper.Map<IEnumerable<UsuarioDto>>(usuarios);
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }

    public async Task<UsuarioDto> BuscarPorIdAsync(uint id, CancellationToken cancellationToken = default)
    {
        try 
        { 
            var usuario = await _unitOfWork.UsuarioRepository.BuscarPorIddAsync(id, cancellationToken);

            return _mapper.Map<UsuarioDto>(usuario);
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }

    public async Task CadastrarAsync(UsuarioCadastroDto usuarioDto, CancellationToken cancellationToken = default)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            var usuario = _mapper.Map<Usuario>(usuarioDto);

            await _unitOfWork.UsuarioRepository.CadastrarAsync(usuario, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        catch(Exception ex)
        {
            await _unitOfWork.RollbackAsync();
            throw ex;
        }
    }

    public async Task AtualizarAsync(UsuarioAtualizacaoDto usuarioDto, CancellationToken cancellationToken = default)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            var usuario = _mapper.Map<Usuario>(usuarioDto);

            await _unitOfWork.UsuarioRepository.AtualizarAsync(usuario, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        catch(Exception ex)
        {
            await _unitOfWork.RollbackAsync();
            throw ex;
        }
    }

    public async Task DeletarAsync(uint id, CancellationToken cancellationToken = default)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            await _unitOfWork.UsuarioRepository.DeletarAsync(id, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();
            throw ex;
        }
    }
}
