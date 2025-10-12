using Financeiro.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Financeiro.Infrastructure.Data.Configurations;

public class PerfilDeAcessoTelaPermissaoConfiguration : IEntityTypeConfiguration<PerfilDeAcessoTelaPermissao>
{
    public void Configure(EntityTypeBuilder<PerfilDeAcessoTelaPermissao> builder)
    {
        builder.HasKey(x => new { x.PerfilDeAcessoId, x.TelaId, x.PermissaoId });

        builder.Ignore(pt => pt.DataCadastro);

        builder.Ignore(pt => pt.DataUltimaAtualizacao);

        builder.HasOne(pt => pt.PerfilDeAcesso)
               .WithMany(p => p.PerfisDeAcessoTelasPermissoes)
               .HasForeignKey(pt => pt.PerfilDeAcessoId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(pt => pt.Tela)
                .WithMany(t => t.PerfisDeAcessoTelasPermissoes)
                .HasForeignKey(pt => pt.TelaId)
                .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(pt => pt.Permissao)
                .WithMany(p => p.PerfisDeAcessoTelasPermissoes)
                .HasForeignKey(pt => pt.PermissaoId)
                .OnDelete(DeleteBehavior.Cascade);
    }
}