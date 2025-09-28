using Financeiro.Domain.Entities;

namespace Financeiro.Domain.Interfaces.Repositories;

public interface IUsuarioRepository : IRepository<Usuario>
{
    Task<Usuario?> BuscarPorRefreshTokenAsync(string email, CancellationToken cancellationToken = default);
    Task<Usuario?> BuscarPorEmailAsync(string email, CancellationToken cancellationToken = default);
}