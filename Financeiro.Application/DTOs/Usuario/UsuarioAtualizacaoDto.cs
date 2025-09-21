namespace Financeiro.Application.DTOs.Usuario;

public record UsuarioAtualizacaoDto
(
    uint UsuarioId,

    string Nome,

    string Email,

    string Senha
);