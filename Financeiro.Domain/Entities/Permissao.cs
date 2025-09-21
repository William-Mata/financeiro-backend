using Financeiro.Domain.Enums;

namespace Financeiro.Domain.Entities;

public class Permissao : BaseEntity
{
    public string Descricao { get; private set; } = string.Empty;

    public StatusPermissao StatusPermissao { get; private set; }
}