using Financeiro.Domain.Entities.Configuracoes;
using Financeiro.Infrastructure.Sercurity;
using Microsoft.Extensions.Options;

namespace Financeiro.API.Middlewares;

public class AutenticacaoOpenAPIMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<AutenticacaoOpenAPIMiddleware> _logger;
    private readonly HMACSettings _hmacSettings;

    public AutenticacaoOpenAPIMiddleware(RequestDelegate next, ILogger<AutenticacaoOpenAPIMiddleware> logger, IOptions<HMACSettings> options)
    {
        _next = next;
        _logger = logger;
        _hmacSettings = options.Value;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            if (RequerAutenticacao(context.Request.Path))
            {
                var assinatura = context.Request.Headers["X-Signature"].FirstOrDefault();
                var dataHoraRequisicao = context.Request.Headers["X-Timestamp"].FirstOrDefault();

                if (string.IsNullOrEmpty(assinatura) || string.IsNullOrEmpty(dataHoraRequisicao))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Cabeçalhos de autenticação ausentes");
                    return;
                }

                assinatura = TratarAssinatura(assinatura);

                context.Request.EnableBuffering();
                var bodyStream = new StreamReader(context.Request.Body);
                var body = await bodyStream.ReadToEndAsync();
                context.Request.Body.Position = 0;

                if (!HMACValidacao.ValidarHMAC(body, assinatura, dataHoraRequisicao, _hmacSettings.SecretKey!))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Não autorizado.");
                    return;
                }
            }

            await _next.Invoke(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao validar HMAC");
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsync("Erro interno do servidor");
            return;
        }
    }

    private string TratarAssinatura(string assinatura)
    {
        if (assinatura.StartsWith("HMAC", StringComparison.OrdinalIgnoreCase))
            assinatura = assinatura.Substring(4);

        if (assinatura.StartsWith("HMAC=", StringComparison.OrdinalIgnoreCase) || assinatura.StartsWith("HMAC ", StringComparison.OrdinalIgnoreCase))
            assinatura = assinatura.Substring(5);

        if (assinatura.StartsWith("SHA256=", StringComparison.OrdinalIgnoreCase) || assinatura.StartsWith("SHA256 ", StringComparison.OrdinalIgnoreCase))
            assinatura = assinatura.Substring(7);

        return assinatura;
    }

    private bool RequerAutenticacao(PathString path)
    {
        return path.HasValue && path.Value.Contains("/api/OpenAPI/");
    }
}