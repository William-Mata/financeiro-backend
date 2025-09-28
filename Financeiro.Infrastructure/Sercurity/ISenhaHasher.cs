namespace Financeiro.Infrastructure.Sercurity;

public interface ISenhaHasher
{
    string SenhaHash(string senha);

    bool VerificarSenha(string senha, string senhaHash);
}