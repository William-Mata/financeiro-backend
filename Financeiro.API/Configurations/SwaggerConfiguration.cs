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
                return fullName.Contains(".OpenApi.");
            }

            if (docName == "interno")
            {
                return true;
            }

            return false;
        });
    }

    //public static void 
}