using Financeiro.Application.DTOs.Usuario;

namespace Financeiro.Application.Services.Interfaces;

public interface IUsuarioService
{
    Task<IEnumerable<UsuarioDto>> ListarAsync(CancellationToken cancellationToken = default);
    Task<UsuarioDto> BuscarPorIdAsync(uint id, CancellationToken cancellationToken = default);
    Task CadastrarAsync(UsuarioCadastroDto usuario, CancellationToken cancellationToken = default);
    Task AtualizarAsync(UsuarioAtualizacaoDto usuario, CancellationToken cancellationToken = default);
    Task DeletarAsync(uint id, CancellationToken cancellationToken = default);
}