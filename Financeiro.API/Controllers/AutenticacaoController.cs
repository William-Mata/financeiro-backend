using Financeiro.Application.DTOs.Autenticacao;
using Financeiro.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Financeiro.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[EnableRateLimiting("AuthPolicy")]
public class AutenticacaoController : ControllerBase
{
    private readonly IAutenticacaoService _autenticacaoService;

    public AutenticacaoController(IAutenticacaoService autenticacaoService)
    {
        _autenticacaoService = autenticacaoService;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginDto login, CancellationToken cancellationToken)
    {
        var result = await _autenticacaoService.LoginAsync(login, cancellationToken);

        if (string.IsNullOrEmpty(result.Token))
        {
            return Unauthorized(new { message = "Não foi possível se autenticar." });
        }

        return Ok(result);
    }


    [HttpPost("Logout")]
    [Authorize]
    public async Task<IActionResult> Logout(CancellationToken cancellationToken)
    {
        var refreshToken = Request.Cookies["refreshToken"];

        if (!string.IsNullOrEmpty(refreshToken))
        {
            await _autenticacaoService.LogoutAsync(refreshToken, cancellationToken);
        }

        Response.Cookies.Delete("refreshToken");

        return Ok();
    }


    [HttpPost("RefreshToken")]
    public async Task<IActionResult> RefreshToken(CancellationToken cancellationToken)
    {
        var refreshToken = Request.Cookies["refreshToken"];
        if (string.IsNullOrEmpty(refreshToken))
        {
            return BadRequest(new { message = "Refresh token não encontrado."});
        }

        var result = await _autenticacaoService.RefreshTokenAsync(refreshToken, cancellationToken);

        if (string.IsNullOrEmpty(result.Token))
        {
            return Unauthorized(new { message = "Não foi possível renovar o token." });
        }

        AlterarCookieRefreshToken(result.RefreshToken);

        return Ok(result);
    }


    private void AlterarCookieRefreshToken(string refreshToken)
    {
        CookieOptions cookieOptions = new()
        {
            HttpOnly = true,
            Expires = DateTime.UtcNow.AddDays(7),
            Secure = true,
            SameSite = SameSiteMode.Strict
        };

        Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
    }
}