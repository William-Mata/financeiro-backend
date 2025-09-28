using Financeiro.Domain.Entities;
using Financeiro.Domain.Interfaces.Repositories;
using Financeiro.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Financeiro.Infrastructure.Repositories;

public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
{
    public UsuarioRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) {  }

    public async Task<Usuario?> BuscarPorRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken, cancellationToken);
    }

    public async Task<Usuario?> BuscarPorEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }
}