using AutoMapper;
using Financeiro.Application.DTOs.Usuario;
using Financeiro.Application.Interfaces;
using Financeiro.Application.Services.Interfaces;
using Financeiro.Domain.Entities;
using Financeiro.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Financeiro.Application.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<UsuarioService> _logger;
    private readonly ISenhaHasher _senhaHash;

    public UsuarioService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UsuarioService> logger, ISenhaHasher senhaHash)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
        _senhaHash = senhaHash;
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
            throw new Exception(ex.Message);
        }
    }

    public async Task<UsuarioDto> BuscarPorIdAsync(uint id, CancellationToken cancellationToken = default)
    {
        try 
        { 
            var usuario = await _unitOfWork.UsuarioRepository.BuscarPorIdAsync(id, cancellationToken);

            return _mapper.Map<UsuarioDto>(usuario);
        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<UsuarioDto> BuscarPorEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        try 
        { 
            var usuario = await _unitOfWork.UsuarioRepository.BuscarPorEmailAsync(email, cancellationToken);

            return _mapper.Map<UsuarioDto>(usuario);
        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task CadastrarAsync(UsuarioCadastroDto usuarioDto, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Iniciando transação para cadastrar um novo usuário.");

            await _unitOfWork.BeginTransactionAsync();

            var usuarioExistente = await _unitOfWork.UsuarioRepository.BuscarPorEmailAsync(usuarioDto.Email, cancellationToken);

            if (usuarioExistente != null)
            {
                _logger.LogWarning("Tentativa de cadastro com email já existente: {Email}", usuarioDto.Email);
                throw new Exception("Já existe um usuário cadastrado com este email.");
            }

            usuarioDto.Senha = _senhaHash.SenhaHash(usuarioDto.Senha);
            var usuario = _mapper.Map<Usuario>(usuarioDto);

            await _unitOfWork.UsuarioRepository.CadastrarAsync(usuario, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync();
        }
        catch(Exception ex)
        {
            await _unitOfWork.RollbackAsync();
            throw new Exception(ex.Message);
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
            await _unitOfWork.CommitAsync();
        }
        catch(Exception ex)
        {
            await _unitOfWork.RollbackAsync();
            throw new Exception(ex.Message);
        }
    }

    public async Task DeletarAsync(uint id, CancellationToken cancellationToken = default)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            await _unitOfWork.UsuarioRepository.DeletarAsync(id, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync();
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();
            throw new Exception(ex.Message);
        }
    }
}
