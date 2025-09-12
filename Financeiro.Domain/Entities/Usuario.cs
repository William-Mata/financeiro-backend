namespace Financeiro.Domain.Entities;

public class Usuario : BaseEntity
{
    public string Nome { get; protected set; }

    public string Email { get; protected set; }

    public string Senha { get; protected set; }

    public Usuario(Usuario? usuarioCadastro) : base(usuarioCadastro)
    {
    }
}
