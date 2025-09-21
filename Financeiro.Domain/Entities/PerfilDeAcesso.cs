namespace Financeiro.Domain.Entities;

public class PerfilDeAcesso : BaseEntity
{
    public string Descricao { get; private set; } = string.Empty;

    public IEnumerable<Tela> Telas { get; private set; } = [];
}