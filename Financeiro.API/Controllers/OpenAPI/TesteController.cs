using Microsoft.AspNetCore.Mvc;

namespace Financeiro.API.Controllers.OpenAPI;

[Route("api/OpenAPI/[controller]")]
[ApiController]
public class TesteController : ControllerBase
{
    public TesteController()
    {
        
    }

    [HttpGet("TesteAPI")]
    public async Task<ActionResult> TesteAPI(CancellationToken cancellationToken)
    {
        try
        { 
            return Ok(new { message = "Sucesso na comunicação com a API." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = "Erro na comunicação com a API."});
        }
    }
}
