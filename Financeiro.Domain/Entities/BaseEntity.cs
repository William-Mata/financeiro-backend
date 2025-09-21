namespace Financeiro.Domain.Entities;

/// <summary>
/// Classe base para todas as entidades de domínio
/// Fornece propriedades comuns como ID
/// </summary>
public class BaseEntity()
{
    public DateTime DataCadastro { get; protected set; }
    public DateTime DataUltimaAtualizacao { get; protected set; }
}