using Financeiro.Domain.Enums;

namespace Financeiro.Domain.Entities;

public class Permissao : BaseEntity
{
    public byte PermissaoId { get; set; }

    public string Descricao { get; private set; } = string.Empty;

    public EStatusPermissao Status { get; private set; }

    public ICollection<PerfilDeAcessoTelaPermissao> PerfisDeAcessoTelasPermissoes { get; private set; } = [];
}