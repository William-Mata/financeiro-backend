namespace Financeiro.Application.DTOs.Autenticacao;

public record AutenticacaoRetornoDto(string Token, string RefreshToken, DateTime DataExpiracaoToken);