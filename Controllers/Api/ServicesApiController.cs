using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BarberBookingWeb.Data;
using BarberBookingWeb.Models;

namespace BarberBookingWeb.Controllers.Api;

[ApiController]
[Route("api/services")]
public class ServicesApiController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ServicesApiController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetServices()
    {
        var services = await _context.BarberServices
            .Include(s => s.Category)
            .Where(s => s.IsApproved)
            .Select(s => new
            {
                s.Id,
                s.Name,
                s.Description,
                s.DurationInMinutes,
                s.Price,
                s.CategoryName,
                CategoryId = s.CategoryId,
                ImageUrl = s.ImageUrl
            })
            .ToListAsync();

        return Ok(services);
    }
}
