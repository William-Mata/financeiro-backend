using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Financeiro.Infrastructure.Sercurity;
public class SenhaHasher : ISenhaHasher
{
    private const sbyte SaltSize = 128 / 8;
    private const sbyte KeySize = 128 / 8;
    private const ushort Iterations = 10000;

    public string SenhaHash(string senha)
    {
        Convert.ToBase64String(RandomNumberGenerator.GetBytes(SaltSize));

        var salt = RandomNumberGenerator.GetBytes(SaltSize);

        var hash = KeyDerivation.Pbkdf2(password: senha,
                                         salt: salt,
                                         prf: KeyDerivationPrf.HMACSHA256,
                                         iterationCount: Iterations,
                                         numBytesRequested: KeySize);

        var hashBytes = new byte[SaltSize + KeySize];

        Array.Copy(salt, 0, hashBytes, 0, SaltSize);
        Array.Copy(hash, 0, hashBytes, SaltSize, KeySize);

        return Convert.ToBase64String(hashBytes);
    }

    public bool VerificarSenha(string senha, string senhaHash)
    {
        var hashBytes = Convert.FromBase64String(senhaHash);

        var salt = new byte[SaltSize];
        Array.Copy(hashBytes, 0, salt, 0, SaltSize);

        var hash = new byte[KeySize];
        Array.Copy(hashBytes, SaltSize, hash, 0, KeySize);

        var hashToCompare = KeyDerivation.Pbkdf2(password: senha,
                                                 salt: salt,
                                                 prf: KeyDerivationPrf.HMACSHA256,
                                                 iterationCount: Iterations,
                                                 numBytesRequested: KeySize);

        return CryptographicOperations.FixedTimeEquals(hash, hashToCompare);
    }
}