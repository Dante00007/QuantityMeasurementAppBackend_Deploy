using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using QuantityMeasurementAppModelLayer.Entity;
using QuantityMeasurementAppModelLayer.DTO;

using QuantityMeasurementAppBusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
namespace QuantityMeasurementAPI.Controller;

[Route("api/quantity")]
[ApiController]
public class QuantityMeasurementController : ControllerBase
{
    private readonly IMeasurementService _measurementService;

    public QuantityMeasurementController(IMeasurementService measurementService)
    {
        _measurementService = measurementService;
    }

    [HttpPost("conversion")]
    public async Task<IActionResult> QuantityMeasurementConversion([FromBody] QuantityDTO newEntity, [FromQuery] string toUnit)
    {
        string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var res = await _measurementService.PerformConversion(newEntity, toUnit, userId);

        return Ok(new
        {
            Value = res.Value,
            Unit = res.Unit
        });
    }

    [HttpPost("addition")]
    public async Task<IActionResult> QuantityMeasurementAddition([FromBody] ArithmeticDTO newEntity, [FromQuery] string toUnit)
    {
        string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var res = await _measurementService.PerformAddition(newEntity.Quantity1, newEntity.Quantity2, toUnit, userId);

        return Ok(new
        {
            Value = res.Value,
            Unit = res.Unit
        });
    }

    [HttpPost("subtraction")]
    public async Task<IActionResult> QuantityMeasurementSubtraction([FromBody] ArithmeticDTO newEntity, [FromQuery] string toUnit)
    {
        string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var res = await _measurementService.PerformSubtraction(newEntity.Quantity1, newEntity.Quantity2, toUnit, userId);

        return Ok(new
        {
            Value = res.Value,
            Unit = res.Unit
        });
    }

    [HttpPost("division")]
    public async Task<IActionResult> QuantityMeasurementDivision([FromBody] ArithmeticDTO newEntity, [FromQuery] string toUnit)
    {
        string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var res = await _measurementService.PerformDivision(newEntity.Quantity1, newEntity.Quantity2, toUnit, userId);

        return Ok(new
        {
            Value = res.Value,
            Unit = res.Unit
        });
    }

    [HttpPost("equality")]
    public async Task<IActionResult> QuantityMeasurementEquality([FromBody] ArithmeticDTO newEntity)
    {
        string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var res = await _measurementService.PerformEquality(newEntity.Quantity1, newEntity.Quantity2);

        return Ok(new
        {
            Result = res
        });
    }
}