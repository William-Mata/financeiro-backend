using Financeiro.Domain.Enums;

namespace Financeiro.Domain.Entities;

public class Usuario : BaseEntity
{
    public uint UsuarioId { get; protected set; }
    public byte PerfilDeAcessoId { get; protected set; }
    public string Nome { get; private set; } 
    public string Email { get; private set; } 
    public string Senha { get; private set; }
    public byte QuantidadeTentativasLogin { get; private set; }
    public string? RefreshToken { get; set; }
    public DateTime? DataExpiracaoRefreshToken { get; set; }
    public DateTime? DataUltimoAcesso { get; private set; }
    public DateTime? DataBloqueio { get; private set; }
    public EStatusUsuario Status {get; private set;}
    public PerfilDeAcesso? PerfilDeAcessos { get; private set; }

    public Usuario(string nome, string email, string senha)
    {
        Nome = nome;
        Email = email;
        Senha = senha;
        Status = EStatusUsuario.Ativo;
        DataCadastro = DateTime.UtcNow;
    }
       
    public void AtualizarRefreshToken(string refreshToken)
    {
        RefreshToken = refreshToken;
        DataExpiracaoRefreshToken = DateTime.UtcNow.AddDays(7);
    }

    public void LimparRefreshToken()
    {
        RefreshToken = null;
        DataExpiracaoRefreshToken = null;
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
        Status = EStatusUsuario.Inativo;
        DataUltimaAtualizacao = DateTime.UtcNow;
    }

    public void Ativar()
    {
        Status = EStatusUsuario.Ativo;
        DataUltimaAtualizacao = DateTime.UtcNow;
    }

    public void Bloquear()
    {
        Status = EStatusUsuario.Bloqueado;
        DataBloqueio = DateTime.UtcNow;
    }

    public void Desbloquear()
    {
        Status = EStatusUsuario.Ativo;
        QuantidadeTentativasLogin = 0;
        DataBloqueio = null;
    }

    public void AdicionarTentativaLogin()
    {
        QuantidadeTentativasLogin++;

        if(QuantidadeTentativasLogin >= 5 && Status != EStatusUsuario.Bloqueado)
            Bloquear();
    }

    public void AlterarDataUltimoAcesso()
    {
        DataUltimoAcesso = DateTime.UtcNow;
    }

    public void ResetarTentativasLogin()
    {
        QuantidadeTentativasLogin = 0;
    }
}