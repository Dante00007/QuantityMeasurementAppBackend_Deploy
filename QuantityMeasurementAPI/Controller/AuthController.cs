using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using QuantityMeasurementAppBusinessLayer.Interface;
using QuantityMeasurementAppBusinessLayer.Services;
using QuantityMeasurementAppModelLayer.DTO;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authServices;
    public AuthController(IAuthService authServices)
    {
        _authServices = authServices;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
    {
        var res = await _authServices.Register(registerDTO);
        return Ok(res);

    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
    {

        var res = await _authServices.Login(loginDTO);
        Response.Cookies.Append("refreshToken", res.RefreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Lax,
            Expires = DateTime.Now.AddDays(7)
        });
        if (res == null)
        {
            return Unauthorized(new { Message = "Invalid email or password." });
        }
        return Ok(new { token = res.AccessToken });
    }
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh()
    {
        string? refreshToken = Request.Cookies["refreshToken"];
        var authHeader = Request.Headers["Authorization"][0].Split(' ')[1];

        var res = await _authServices.Refresh(authHeader, refreshToken);

        return Ok(new { token = res.AccessToken });
    }

    [HttpDelete("logout")]
    public async Task<IActionResult> Logout()
    {
        string? refreshToken = Request.Cookies["refreshToken"];
        string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await _authServices.Logout(userId, refreshToken);
        Response.Cookies.Delete("refreshToken");
        return Ok(new { Message = "Logged out successfully." });
    }
}