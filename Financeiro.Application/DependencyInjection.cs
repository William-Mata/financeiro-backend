using Financeiro.Application.Services;
using Financeiro.Application.Services.Interfaces;
using Financeiro.Application.Validators.Usuario;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;

namespace Financeiro.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg => cfg.AddMaps(typeof(DependencyInjection).Assembly));

        services.AddScoped<IUsuarioService, UsuarioService>();
        services.AddScoped<IAutenticacaoService, AutenticacaoService>();

        return services;
    }

    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<UsuarioCadastroValidator>();
        services.AddValidatorsFromAssemblyContaining<UsuarioAlteracaoValidator>();

        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();

        return services;
    }
}