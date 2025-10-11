using Financeiro.Application.DTOs.Autenticacao;
using Financeiro.Application.Interfaces;
using Financeiro.Application.Services.Interfaces;
using Financeiro.Domain.Enums;
using Financeiro.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Financeiro.Application.Services;

public class AutenticacaoService : IAutenticacaoService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISenhaHasher _senhaHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly ILogger<AutenticacaoService> _logger;

    public AutenticacaoService(IUnitOfWork unitOfWork, ISenhaHasher senhaHasher,  IJwtTokenGenerator jwtTokenGenerator, ILogger<AutenticacaoService> logger)
    {
        _unitOfWork = unitOfWork;
        _senhaHasher = senhaHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
        _logger = logger;
    }

    public async Task<AutenticacaoRetornoDto> LoginAsync(LoginDto login, CancellationToken cancellationToken)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            _logger.LogInformation("Iniciando processo de login para o usuário com email: {Email}", login.Email);

            var usuario = await _unitOfWork.UsuarioRepository.BuscarPorEmailAsync(login.Email, cancellationToken);

            if (usuario == null)
            {
                _logger.LogWarning("Usuário com email {Email} não encontrado.", login.Email);
                throw new UnauthorizedAccessException("Credenciais inválidas.");
            }
            else if (usuario.Status == StatusUsuario.Bloqueado)
            {
                _logger.LogWarning("Usuário com email {Email} está bloqueado.", login.Email);
                throw new UnauthorizedAccessException("Usuário bloqueado.");
            }
            else if (!_senhaHasher.VerificarSenha(login.Senha, usuario.Senha))
            {
                _logger.LogWarning("Senha inválida para o usuário com email {Email}.", login.Email);
                throw new UnauthorizedAccessException("Credenciais inválidas.");
            }

            usuario.ResetarTentativasLogin();
            usuario.AlterarDataUltimoAcesso();

            var tokenAcesso = _jwtTokenGenerator.GerarToken(usuario.UsuarioId, usuario.Email);
            var refreshToken = _jwtTokenGenerator.GerarRefreshToken();

            usuario.AtualizarRefreshToken(refreshToken);
            await _unitOfWork.UsuarioRepository.AtualizarAsync(usuario, cancellationToken);

            _logger.LogInformation("Usuário com email {Email} autenticado com sucesso.", login.Email);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return new AutenticacaoRetornoDto(tokenAcesso.token, refreshToken, tokenAcesso.expiracaoToken);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();
            _logger.LogError(ex, "Erro durante o processo de login para o usuário com email: {Email}", login.Email);
            throw;
        }
    }

    public async Task<AutenticacaoRetornoDto> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            var usuario = await _unitOfWork.UsuarioRepository.BuscarPorRefreshTokenAsync(refreshToken, cancellationToken);

            if (usuario == null || usuario.DataExpiracaoRefreshToken <= DateTime.UtcNow || usuario.Status == StatusUsuario.Bloqueado)
            {
                throw new UnauthorizedAccessException("Refresh token inválido ou usuário bloqueado.");
            }

            var tokenAcesso = _jwtTokenGenerator.GerarToken(usuario.UsuarioId, usuario.Email);
            var novoRefreshToken = _jwtTokenGenerator.GerarRefreshToken();
            usuario.AtualizarRefreshToken(novoRefreshToken);

            await _unitOfWork.UsuarioRepository.AtualizarAsync(usuario, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);

            return new AutenticacaoRetornoDto(tokenAcesso.token, novoRefreshToken, tokenAcesso.expiracaoToken);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();
            _logger.LogError(ex, "Erro durante o processo de refresh token.");
            throw;
        }
    }

    public async Task LogoutAsync(string refreshToken, CancellationToken cancellationToken)
    {
        try
        {
            var usuario = await _unitOfWork.UsuarioRepository.BuscarPorRefreshTokenAsync(refreshToken, cancellationToken);

            if(usuario != null)
            {
                usuario.LimparRefreshToken();

                await _unitOfWork.UsuarioRepository.AtualizarAsync(usuario, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitAsync(cancellationToken);
            }
        }   
        catch(Exception ex)
        {
            await _unitOfWork.RollbackAsync();
            _logger.LogError(ex, "Erro durante o processo de logout.");
            throw;
        }
    }
}