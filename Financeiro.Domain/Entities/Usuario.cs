using Financeiro.Domain.Enums;

namespace Financeiro.Domain.Entities;

public class Usuario : BaseEntity
{
    public uint UsuarioId { get; protected set; }
    public string Nome { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Senha { get; private set; } = string.Empty;

    public StatusUsuario Status {get; private set;}

    public IEnumerable<PerfilDeAcesso> PerfilDeAcessos { get; private set; } = new List<PerfilDeAcesso>();

    private Usuario(){}

    public Usuario(string nome, string email, string senha, StatusUsuario status)
    {
        Nome = nome;
        Email = email;
        Senha = senha;
        Status = status;
        DataCadastro = DateTime.UtcNow;
    }

    public void AtualizarNome(string nome) 
    {
        Nome = nome;
        DataUltimaAtualizacao = DateTime.UtcNow;
    }
    
    public void AtualizarEmail(string email)
    {
        Email = email;
        DataUltimaAtualizacao = DateTime.UtcNow;
    }

    public void AlterarSenha(string senha)
    {
        Senha = senha;
        DataUltimaAtualizacao = DateTime.UtcNow;
    }

    public void Inativar()
    {
        Status = StatusUsuario.Inativo;
        DataUltimaAtualizacao = DateTime.UtcNow;
    }

    public void Ativar()
    {
        Status = StatusUsuario.Ativo;
        DataUltimaAtualizacao = DateTime.UtcNow;
    }
}