using kolokwium2.DTOs;
using kolokwium2.Exceptions;
using kolokwium2.Services;
using Microsoft.AspNetCore.Mvc;

namespace kolokwium2.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly IDbService _dbService;

    public CustomersController(IDbService dbService)
    {
        _dbService = dbService;
    }

    [HttpGet("{customerId}/purchases")]
    public async Task<IActionResult> GetCustomerPurchases(int customerId)
    {
        try
        {
            var res = await _dbService.GetCustomerPurchasesAsync(customerId);
            return Ok(res);
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