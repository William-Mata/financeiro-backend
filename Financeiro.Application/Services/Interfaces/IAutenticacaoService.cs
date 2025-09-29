using Financeiro.Application.DTOs.Autenticacao;

namespace Financeiro.Application.Services.Interfaces;

public interface IAutenticacaoService 
{
    Task<AutenticacaoRetornoDto> LoginAsync(LoginDto login, CancellationToken cancellationToken);
    Task<AutenticacaoRetornoDto> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken);
    Task LogoutAsync(string refreshToken, CancellationToken cancellationToken);
}