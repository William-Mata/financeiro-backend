using Financeiro.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Financeiro.Infrastructure.Data.Configurations;

public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.HasKey(u => u.UsuarioId);

        builder.Property(u => u.UsuarioId)
           .ValueGeneratedNever();

        builder.Property(u => u.Nome)
                .IsRequired()
                .HasMaxLength(250);

        builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(250);

        builder.Property(u => u.Senha)
                .IsRequired();

        builder.Property(u => u.Status)
                .HasConversion<string>()
                .IsRequired();

        builder.Property(u => u.DataCadastro)
                .IsRequired();

        builder.Property(u => u.DataUltimaAtualizacao);

        builder.Ignore(x => x.PerfilDeAcessos);
    }
}