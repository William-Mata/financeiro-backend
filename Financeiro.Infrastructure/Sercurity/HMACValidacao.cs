using System.Security.Cryptography;
using System.Text;

namespace Financeiro.Infrastructure.Sercurity;

public static class HMACValidacao
{
    private const int HMAC_SHA256_SIZE_BYTES = 32; 
    private const int MIN_SECRET_KEY_LENGTH = 32;
    private const int MINUTOS_VALIDOS_DATA_HORA_REQUISICAO = 10;

    public static string GerarHMAC(string message, string secretKey)
    {
        var keyBytes = Encoding.UTF8.GetBytes(secretKey);
        var messageBytes = Encoding.UTF8.GetBytes(message);

        using (var hmac = new HMACSHA256(keyBytes))
        {
            var hashBytes = hmac.ComputeHash(messageBytes);
            return Convert.ToBase64String(hashBytes);
        }
    }

    public static bool ValidarHMAC(string body, string? assinatura, string? dataHoraRequisicao, string secretKey)
    {
        if (string.IsNullOrEmpty(secretKey))
            return false;

        if (IsValidoDataHoraRequisicao(dataHoraRequisicao))
            return false;

        if (!IsValidoSecretKey(secretKey))
            return false;

        if (!IsValidoBase64(assinatura))
            return false;

        if (!IsValidoHMACTamanhoAssinatura(assinatura))
            return false;

        var expectedSignature = GerarHMAC(body, secretKey);

        return IsHMACAssinaturaIgual(assinatura, expectedSignature);
    }

    private static bool IsValidoDataHoraRequisicao(string? dataHoraRequisicao)
    {
        if (long.TryParse(dataHoraRequisicao, out long unixTimestamp))
        {
            var requestTime = DateTimeOffset.FromUnixTimeSeconds(unixTimestamp);

            if (DateTimeOffset.UtcNow.Subtract(requestTime).TotalMinutes > MINUTOS_VALIDOS_DATA_HORA_REQUISICAO)
                return false;
        }

        return true;
    }

    public static bool IsValidoBase64(string input)
    {
        if (string.IsNullOrEmpty(input))
            return false;

        try
        {
            if (input.Length % 4 != 0)
                return false;

            var buffer = Convert.FromBase64String(input);
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }

    public static bool IsValidoHMACTamanhoAssinatura(string base64Signature)
    {
        try
        {
            var bytes = Convert.FromBase64String(base64Signature);
            return bytes.Length == HMAC_SHA256_SIZE_BYTES;
        }
        catch
        {
            return false;
        }
    }

    public static bool IsValidoSecretKey(string secretKey)
    {
        return !string.IsNullOrEmpty(secretKey) &&
               secretKey.Length >= MIN_SECRET_KEY_LENGTH;
    }

    private static bool IsHMACAssinaturaIgual(string a, string b)
    {
        if (a == null || b == null || a.Length != b.Length)
            return false;

        var result = 0;
        for (int i = 0; i < a.Length; i++)
        {
            result |= a[i] ^ b[i];
        }
        return result == 0;
    }
}