using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BarberBookingWeb.Data;
using BarberBookingWeb.Models;
using Microsoft.AspNetCore.Authorization;

namespace BarberBookingWeb.Controllers.Api;

[ApiController]
[Route("api/admin")]
[Authorize] // Yêu cầu đăng nhập tài khoản quản trị Admin
public class AdminApiController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public AdminApiController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("bookings")]
    public async Task<IActionResult> GetAllBookings()
    {
        var bookings = await _context.Bookings
            .Include(b => b.BookingServices).ThenInclude(bs => bs.BarberService)
            .OrderByDescending(b => b.StartTime)
            .Select(b => new
            {
                b.Id,
                b.CustomerName,
                b.CustomerPhone,
                b.StartTime,
                b.EndTime,
                b.Status,
                Services = b.BookingServices.Select(bs => bs.BarberService!.Name).ToList()
            })
            .ToListAsync();

        return Ok(bookings);
    }
}
