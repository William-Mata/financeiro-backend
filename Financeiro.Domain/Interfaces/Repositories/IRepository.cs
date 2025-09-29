namespace Financeiro.Domain.Interfaces.Repositories;

/// <summary>
/// Repositorio generico com metodos padrões para todas os repositorios.
/// </summary>
public interface IRepository <T> where T : class
{
    Task<IEnumerable<T>> ListarAsync(CancellationToken cancellationToken = default);
    Task<T?> BuscarPorIdAsync(uint Id, CancellationToken cancellationToken = default);
    Task<T> CadastrarAsync(T entity, CancellationToken cancellationToken = default);
    Task<T> AtualizarAsync(T entity, CancellationToken cancellationToken = default);
    Task DeletarAsync(uint Id, CancellationToken cancellationToken = default);
}