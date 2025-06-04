using kolokwium2.DTOs;
using kolokwium2.Exceptions;
using kolokwium2.Services;
using Microsoft.AspNetCore.Mvc;

namespace kolokwium2.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WashingMachinesController : ControllerBase
{
    private readonly IDbService _dbService;

    public WashingMachinesController(IDbService dbService)
    {
        _dbService = dbService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateWashingMachineAsync(CreateWashingMachineRequestDto requestDto)
    {
        try
        {
            await _dbService.CreateWashingMachineAsync(requestDto);
            return Created();
        }
        catch (BadRequestException e)
        {
            return BadRequest(e.Message);
        }
        catch (ConflictException e)
        {
            return Conflict(e.Message);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500,"Internal Server Error");
        }
    }
    
}