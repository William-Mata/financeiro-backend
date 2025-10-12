using Financeiro.Domain.Enums;

namespace Financeiro.Domain.Entities;

public class PerfilDeAcesso : BaseEntity
{
    public byte PerfilAcessoId { get; private set; }
    
    public string Descricao { get; private set; } = string.Empty;

    public EStatusPerfilDeAcesso Status { get; private set; }

    public ICollection<Usuario> Usuarios { get; private set; } = [];
    public ICollection<PerfilDeAcessoTelaPermissao> PerfisDeAcessoTelasPermissoes { get; private set; } = [];
}