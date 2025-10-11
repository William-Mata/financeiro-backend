using Financeiro.API.Configurations;
using Financeiro.API.Middlewares;

namespace Financeiro.API;

public static class DependencyInjection
{
    public static IServiceCollection AddAPI(this IServiceCollection services)
    {
        services.AddControllers();
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
        if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
        {
            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/integracoes/swagger.json", "API Integracoes v1");
                s.RoutePrefix = "swagger"; 

                s.DefaultModelsExpandDepth(-1);
                s.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List);
            });

            app.MapWhen(
                context => context.Request.Path.StartsWithSegments("/swagger/interno"),
                appBuilder =>
                {
                    appBuilder.UseSwaggerUI(c =>
                    {
                        c.SwaggerEndpoint("/swagger/interno/swagger.json", "API Interno v1");
                        c.RoutePrefix = "swagger/interno"; 
                        c.DocumentTitle = "API Interno - Documentação";
                        c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List);
                    });
                }
            );
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.MapHealthChecks("/health");

        app.UseMiddleware<AuditoriaMiddleware>();
        app.UseMiddleware<AutenticacaoOpenAPIMiddleware>();

        return app;
    }
}