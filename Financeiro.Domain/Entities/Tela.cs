using Financeiro.Domain.Enums;

namespace Financeiro.Domain.Entities;

public class Tela : BaseEntity
{
    public byte TelaId { get; private set; }
    public string Nome { get; private set; } = string.Empty;
    public string Descricao { get; private set; } = string.Empty;
    public EStatusTela Status { get; private set; }
    public ICollection<PerfilDeAcessoTelaPermissao> PerfisDeAcessoTelasPermissoes { get; private set; } = [];
}