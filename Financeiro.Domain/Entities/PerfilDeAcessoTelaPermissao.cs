namespace Financeiro.Domain.Entities;

public class PerfilDeAcessoTelaPermissao : BaseEntity
{
    public byte PerfilDeAcessoId { get; set; }
    public byte TelaId { get; set; }
    public byte PermissaoId { get; set; }

    public PerfilDeAcesso PerfilDeAcesso { get; set; } = null!;
    public Tela Tela { get; set; } = null!;
    public Permissao Permissao { get; set; } = null!;
}