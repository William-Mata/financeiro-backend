using Financeiro.Application.Interfaces;
using Financeiro.Domain.Interfaces;
using Financeiro.Domain.Interfaces.Repositories;
using Financeiro.Infrastructure.Data;
using Financeiro.Infrastructure.Repositories;
using Financeiro.Infrastructure.Sercurity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Financeiro.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        #region Configuracores
        services.AddScoped<ISenhaHasher, SenhaHasher>();
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        #endregion

        #region Repositorios
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        #endregion

        return services;
    }
}