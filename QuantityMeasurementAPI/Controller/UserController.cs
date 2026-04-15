using Microsoft.AspNetCore.Mvc;

using QuantityMeasurementAppBusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using QuantityMeasurementAppBusinessLayer.Services;
using System.Security.Claims;

namespace QuantityMeasurementAPI.Controller;

[Route("api/user")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("history"), Authorize]
    public async Task<IActionResult> GetHistory()
    {

        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var result = await _userService.GetHistory(userIdClaim);
        return Ok(new { History = result });
    }


}