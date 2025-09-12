namespace Financeiro.Domain.Entities;

/// <summary>
/// Classe base para todas as entidades de domínio
/// Fornece propriedades e funcionalidades comuns como ID, eventos de domínio e auditoria
/// </summary>
public abstract class BaseEntity
{
    public long Id {get; protected set;}

    public Usuario? UsuarioCadastro {get; protected set;}

    public DateTime DataCadastro {get; protected set;}

    public Usuario? UsuarioUltimaAlteracao { get; protected set; }

    public DateTime? DataUltimaModificacao {get; protected set;}


    public BaseEntity(Usuario? usuarioCadastro)
    {
        UsuarioCadastro = usuarioCadastro;
        DataCadastro = DateTime.UtcNow;
    }

    public void SetUsuarioUltimaAlteracao(Usuario? usuarioAlteracao)
    {
        UsuarioUltimaAlteracao = usuarioAlteracao;
        DataUltimaModificacao = DateTime.UtcNow;
    }
}