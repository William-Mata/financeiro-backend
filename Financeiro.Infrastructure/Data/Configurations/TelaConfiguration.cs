using Financeiro.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Financeiro.Infrastructure.Data.Configurations;

public class TelaConfiguration : IEntityTypeConfiguration<Tela>
{
    public void Configure(EntityTypeBuilder<Tela> builder)
    {
        builder.HasKey(u => u.TelaId);

        builder.Property(u => u.TelaId).IsRequired();

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