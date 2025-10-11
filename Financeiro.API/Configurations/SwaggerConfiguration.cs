using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Financeiro.API.Configurations;

public static class SwaggerConfiguration
{
    public static void DocumentacaoSwagger(SwaggerGenOptions swaggerGenOptions)
    {
        swaggerGenOptions.SwaggerDoc("integracoes", new OpenApiInfo
        {
            Title = "Financeiro API - Integracoes",
            Version = "v1",
            Description = "API pública para integração com parceiros e sistemas externos",
            Contact = new OpenApiContact
            {
                Name = "Suporte API",
                Email = "api@financeiro.com"
            }
        });

        // Documento INTERNO (todas as controllers)
        swaggerGenOptions.SwaggerDoc("interno", new OpenApiInfo
        {
            Title = "Financeiro API - Interno",
            Version = "v1",
            Description = "API completa para uso interno"
        });

        // Filtro para separar os documentos
        swaggerGenOptions.DocInclusionPredicate((docName, apiDesc) =>
        {
            var controllerNamespace = apiDesc.ActionDescriptor
                .RouteValues["controller"];

            var fullName = apiDesc.ActionDescriptor.DisplayName ?? "";

            if (docName == "integracoes")
            {
                return fullName.Contains(".OpenAPI");
            }

            if (docName == "interno")
            {
                return true;
            }

            return false;
        });
    }

    public static WebApplication SwaggerUIConfiguracao(this WebApplication app)
    {
        if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
        {
            app.UseSwagger();

            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/integracoes/swagger.json", "API Integracoes v1");
                s.RoutePrefix = "swagger/OpenAPI";

                s.DefaultModelsExpandDepth(-1);
                s.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List);
            });

            app.MapWhen(
                context => context.Request.Path.StartsWithSegments("/swagger"),
                appBuilder =>
                {
                    appBuilder.UseSwaggerUI(c =>
                    {
                        c.SwaggerEndpoint("/swagger/interno/swagger.json", "API Interno v1");
                        c.RoutePrefix = "swagger";
                        c.DocumentTitle = "API Interno - Documentação";
                        c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List);
                    });
                }
            );
        }

        return app;
    }
}