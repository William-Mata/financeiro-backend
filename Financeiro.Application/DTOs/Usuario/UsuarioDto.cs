namespace Financeiro.Application.DTOs.Usuario;

public record UsuarioDto
{
    public uint UsuarioId { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Status { get; set; }
    public DateTime DataCadastro { get; set; }
    public DateTime? DataUltimaAtualizacao { get; set; }
};