using Financeiro.Application.Services;
using Financeiro.Application.Services.Interfaces;
using Financeiro.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Financeiro.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg => cfg.AddMaps(typeof(DependencyInjection).Assembly));

        services.AddScoped<IUsuarioService, UsuarioService>();

        return services;
    }
}