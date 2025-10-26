using Financeiro.API.Configurations;
using Financeiro.API.Middlewares;
using Financeiro.Application;

namespace Financeiro.API;

public static class DependencyInjection
{
    public static IServiceCollection AddAPI(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddValidators();
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(s =>
        {
            SwaggerConfiguration.DocumentacaoSwagger(s);
        });
        
        services.AddHealthChecks();

        return services;
    }

    public static WebApplication AddConfigurations(this WebApplication app)
    {
        app.SwaggerUIConfiguracao();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.MapHealthChecks("/health");

        app.UseMiddleware<AuditoriaMiddleware>();
        app.UseMiddleware<AutenticacaoOpenAPIMiddleware>();

        return app;
    }
}