using Financeiro.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Financeiro.Infrastructure.Data.Configurations;

public class PerfilDeAcessoConfiguration : IEntityTypeConfiguration<PerfilDeAcesso>
{
    public void Configure(EntityTypeBuilder<PerfilDeAcesso> builder)
    {
        builder.HasKey(u => u.PerfilAcessoId);

        builder.Property(u => u.PerfilAcessoId).IsRequired();

        builder.Property(u => u.Descricao)
                .IsRequired()
                .HasMaxLength(255);

        builder.Property(u => u.Status)
                .HasConversion<string>()
                .IsRequired();

        builder.Property(u => u.DataCadastro)
                .IsRequired();

        builder.Property(u => u.DataUltimaAtualizacao);

        #region INDICES
        builder.HasIndex(i => i.Descricao)
             .IsUnique();
        #endregion
    }
}