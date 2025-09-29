using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Financeiro.Application.Interfaces;
using Financeiro.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Financeiro.Infrastructure.Sercurity;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtSettings _jwtSettings;

    public JwtTokenGenerator(IOptions<JwtSettings> jwtOptions)
    {
        _jwtSettings = jwtOptions.Value;
    }

    public (string token, DateTime expiracaoToken) GerarToken(uint usuarioId, string email)
    {
        var dataExpiracaoToken = DateTime.UtcNow.AddMinutes(_jwtSettings.MinutosExpiracaoToken);

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
            Convert.FromBase64String(_jwtSettings.SecretKey)),
            SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, usuarioId.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var securityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.MinutosExpiracaoToken),
            signingCredentials: signingCredentials);

        return (new JwtSecurityTokenHandler().WriteToken(securityToken), dataExpiracaoToken);
    }

    public string GerarRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
    }
}