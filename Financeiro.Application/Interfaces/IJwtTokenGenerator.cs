namespace Financeiro.Application.Interfaces;

public interface IJwtTokenGenerator
{
    (string token, DateTime expiracaoToken) GerarToken(uint UsuarioId, string email);
    string GerarRefreshToken();
}