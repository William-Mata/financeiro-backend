using Financeiro.Domain.Interfaces;
using Financeiro.Domain.Interfaces.Repositories;
using Financeiro.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace Financeiro.Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
{

    private readonly ApplicationDbContext _appDbcontext;
    private IDbContextTransaction? _transaction;
    private IUsuarioRepository? _usuarioRepository;

    public UnitOfWork(ApplicationDbContext appDbContext)
    {
        _appDbcontext = appDbContext;
    }

    public IUsuarioRepository UsuarioRepository => _usuarioRepository ??= new UsuarioRepository(_appDbcontext);


    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _appDbcontext.SaveChangesAsync(cancellationToken);
    }

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        await _appDbcontext.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        await _appDbcontext.Database.CommitTransactionAsync(cancellationToken);
    }

    public async Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        await _appDbcontext.Database.RollbackTransactionAsync(cancellationToken);   
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _appDbcontext.Dispose();
    }
}