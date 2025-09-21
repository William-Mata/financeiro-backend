using Financeiro.Domain.Enums;

namespace Financeiro.Domain.Entities;

public class Tela : BaseEntity
{
    public string Nome { get; private set; } = string.Empty;
    public string Descricao { get; private set; } = string.Empty;
    public StatusTela StatusTela { get; private set; }
    public IEnumerable<Permissao> Permissoes { get; private set; } = [];
}