using Financeiro.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Financeiro.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuario => Set<Usuario>();
    public DbSet<PerfilDeAcesso> PerfilDeAcesso => Set<PerfilDeAcesso>();
    public DbSet<Tela> Tela => Set<Tela>();
    public DbSet<Permissao> Permissao => Set<Permissao>();
    public DbSet<PerfilDeAcessoTelaPermissao> PerfilDeAcessoTelaPermissao => Set<PerfilDeAcessoTelaPermissao>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}