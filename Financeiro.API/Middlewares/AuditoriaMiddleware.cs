using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace Financeiro.API.Middlewares;

public class AuditoriaMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<AuditoriaMiddleware> _logger;

    public AuditoriaMiddleware(RequestDelegate next, ILogger<AuditoriaMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {

        _logger.LogInformation($@"IP: {context.Connection.RemoteIpAddress?.ToString()}
                                  Data e Hora: {DateTime.UtcNow.ToString("dd/MM/yyyy hh:MM:ss")}
                                  Metodo: {context.Request.Method}
                                  Caminho: {context.Request.Path}
                                  QueryString: {context.Request.QueryString.ToString()}
                                  Body: {context.Request.Body?.ToString()}\n");
        
        await _next(context);


        _logger.LogInformation($@"IP: {context.Connection.RemoteIpAddress?.ToString()}
                                Data e Hora: {DateTime.UtcNow.ToString("dd/MM/yyyy hh:MM:ss")}
                                Controller: {context.Request.Host}
                                Metodo: {context.Request.Method}
                                Caminho: {context.Request.Path}
                                QueryString: {context.Request.QueryString.ToString()}
                                Body: {context.Request.Body?.ToString()}
                                Response Status: {context.Response.StatusCode}
                                Response: {context.Response.Body?.ToString()}");
        }
}