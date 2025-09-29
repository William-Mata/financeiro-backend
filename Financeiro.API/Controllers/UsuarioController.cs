using Financeiro.Application.DTOs.Usuario;
using Financeiro.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Financeiro.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly ILogger<UsuarioController> _logger;
    private readonly IUsuarioService _usuarioService;

    public UsuarioController(ILogger<UsuarioController> logger, IUsuarioService usuarioService)
    {
        _logger = logger;
        _usuarioService = usuarioService;
    }

    [HttpGet("ListarAsync")]
    public async Task<ActionResult<IEnumerable<UsuarioDto>>> ListarAsync(CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Buscando todos os usuarios.");

            IEnumerable<UsuarioDto> usuarios = await _usuarioService.ListarAsync(cancellationToken);

            return Ok(usuarios);
        }
        catch (Exception ex)
        {
            _logger.LogTrace(ex, "Erro ao buscar todos os usuarios.");
            return BadRequest(new { message = "Erro ao buscar todos os usuarios." });
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UsuarioDto>> BuscarPorIdAsync(uint id, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Buscando um usuario.");
            UsuarioDto usuario = await _usuarioService.BuscarPorIdAsync(id, cancellationToken);

            return Ok(usuario);
        }
        catch (Exception ex)
        {
            _logger.LogTrace(ex, "Erro ao buscar todos os usuarios.");
            return BadRequest(new { message = "Erro ao buscar todos os usuarios." });
        }
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] UsuarioCadastroDto usuario, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Cadastrando um usuario.");
            await _usuarioService.CadastrarAsync(usuario, cancellationToken);

            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogTrace(ex, "Erro ao cadastrar o usuario.");
            return BadRequest(new { message = "Erro ao cadastrar o usuario." });
        }
    }

    [HttpPut()]
    public async Task<ActionResult> Put([FromBody] UsuarioAtualizacaoDto usuario, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Atualizando um usuario.");
            await _usuarioService.AtualizarAsync(usuario, cancellationToken);

            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogTrace(ex, "Erro ao atualizar o usuario.");
            return BadRequest(new { message = "Erro ao atualizar o usuario." });
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(uint id, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Deletando um usuario.");
            await _usuarioService.DeletarAsync(id, cancellationToken);

            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogTrace(ex, "Erro ao deletar o usuario.");
            return BadRequest(new { message = "Erro ao deletar o usuarios." });
        }
    }
}