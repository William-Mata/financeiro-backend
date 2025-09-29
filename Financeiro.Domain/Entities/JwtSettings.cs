namespace Financeiro.Domain.Entities;

public class JwtSettings
{
    public string? SecretKey { get; init; } = string.Empty;
    public string Issuer { get; init; } = string.Empty;
    public string Audience { get; init; } = string.Empty;
    public ushort MinutosExpiracaoToken { get; init; } = 15;
}