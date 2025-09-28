namespace Financeiro.Application.DTOs.Usuario;

public record UsuarioDto(uint UsuarioId, string Nome, string Email, string UsuarioCadastro, string Status, DateTime DataCadastro, DateTime? DataUltimaModificacao);