using Financeiro.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Financeiro.Infrastructure.Data.Configurations;

public class PermissaoConfiguration : IEntityTypeConfiguration<Permissao>
{
    public void Configure(EntityTypeBuilder<Permissao> builder)
    {
        builder.HasKey(u => u.PermissaoId);

        builder.Property(u => u.PermissaoId)
            .IsRequired();

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