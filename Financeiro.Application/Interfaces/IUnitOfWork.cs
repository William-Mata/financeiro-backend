using Financeiro.Domain.Entities;
using Financeiro.Domain.Interfaces.Repositories;

namespace Financeiro.Domain.Interfaces;

/// <summary>
/// Vantagens do Padrão Unit of Work
/// Consistência → evita salvar alterações parciais.
/// Transações → suporte nativo a Commit e Rollback.
/// Desacoplamento → a aplicação conversa só com IUnitOfWork, e ele gerencia os repositórios.
/// Organização → centraliza SaveChangesAsync no lugar certo.
/// </summary>
public interface IUnitOfWork : IDisposable
{
    IUsuarioRepository UsuarioRepository { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitAsync(CancellationToken cancellationToken = default);
    Task RollbackAsync(CancellationToken cancellationToken = default);
}