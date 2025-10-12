using Financeiro.Domain.Entities;
using Financeiro.Domain.Interfaces.Repositories;
using Financeiro.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Financeiro.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : BaseEntity, new()
{
    protected readonly ApplicationDbContext _applicationDbContext;
    protected readonly DbSet<T> _dbSet;

    public Repository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
        _dbSet = applicationDbContext.Set<T>();
    }

    public async Task<IEnumerable<T>> ListarAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet.ToListAsync(cancellationToken);
    }

    public async Task<T?> BuscarPorIdAsync(uint Id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FindAsync([Id], cancellationToken);
    }

    public async Task<T> CadastrarAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
        await _applicationDbContext.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<T> AtualizarAsync(T entity, CancellationToken cancellationToken = default)
    {
        _dbSet.Update(entity);
        await _applicationDbContext.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task DeletarAsync(uint Id, CancellationToken cancellationToken = default)
    {
        var entity = await this.BuscarPorIdAsync(Id, cancellationToken);
        if(entity != null)
        {
            _dbSet.Remove(entity);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}