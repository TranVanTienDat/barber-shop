using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BarberBookingWeb.Data;
using BarberBookingWeb.Models;

namespace BarberBookingWeb.Controllers.Api;

[ApiController]
[Route("api/bookings")]
public class BookingsApiController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public BookingsApiController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateBooking([FromBody] CreateBookingDto request)
    {
        if (!ModelState.IsValid || request.ServiceIds == null || !request.ServiceIds.Any())
        {
            return BadRequest("Dữ liệu không hợp lệ hoặc chưa chọn dịch vụ.");
        }

        var selectedServices = await _context.BarberServices
            .Where(s => request.ServiceIds.Contains(s.Id))
            .ToListAsync();

        if (selectedServices.Count != request.ServiceIds.Count)
        {
            return BadRequest("Một hoặc nhiều dịch vụ không hợp lệ.");
        }

        int totalDuration = selectedServices.Sum(s => s.DurationInMinutes);

        var booking = new Booking
        {
            Id = Guid.NewGuid(),
            CustomerName = request.CustomerName,
            CustomerPhone = request.CustomerPhone,
            CustomerEmail = request.CustomerEmail,
            Notes = request.Notes,
            StartTime = request.StartTime,
            EndTime = request.StartTime.AddMinutes(totalDuration),
            Status = BookingStatus.Pending,
            BookingServices = selectedServices.Select(s => new BookingService
            {
                ServiceId = s.Id
            }).ToList()
        };

        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync();

        return Created($"/api/bookings/{booking.Id}", new { booking.Id, booking.Status, booking.StartTime });
    }

    [HttpGet("user")]
    public async Task<IActionResult> GetUserBookings([FromQuery] string? phone, [FromQuery] string? email)
    {
        // Trong trường hợp này, nếu ta đang dùng cho app ngoài và chưa có auth, 
        // ta có thể cho lấy lịch đã đặt qua tham số phone hoặc email.
        if (string.IsNullOrEmpty(phone) && string.IsNullOrEmpty(email))
        {
            // Trả về theo user đã đăng nhập nếu hệ thống có phiên (Session/CookieAuth) 
            var username = User.Identity?.Name;
            if (string.IsNullOrEmpty(username))
            {
                return Unauthorized("Vui lòng đăng nhập hoặc cung cấp số điện thoại/email để xem lịch hẹn.");
            }
            phone = username; // Giả sử username đăng nhập là sđt
        }

        var query = _context.Bookings
            .Include(b => b.BookingServices).ThenInclude(bs => bs.BarberService)
            .AsQueryable();

        if (!string.IsNullOrEmpty(phone))
            query = query.Where(b => b.CustomerPhone == phone);

        if (!string.IsNullOrEmpty(email))
            query = query.Where(b => b.CustomerEmail == email);

        var userBookings = await query
            .OrderByDescending(b => b.StartTime)
            .Select(b => new
            {
                b.Id,
                b.CustomerName,
                b.StartTime,
                b.EndTime,
                b.Status,
                Services = b.BookingServices.Select(bs => bs.BarberService!.Name).ToList()
            })
            .ToListAsync();

        return Ok(userBookings);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> CancelBooking(Guid id)
    {
        var booking = await _context.Bookings.FindAsync(id);
        if (booking == null) return NotFound("Không tìm thấy lịch hẹn.");

        if (booking.Status == BookingStatus.Cancelled)
        {
            return BadRequest("Lịch hẹn này đã bị huỷ trước đó.");
        }

        booking.Status = BookingStatus.Cancelled;
        await _context.SaveChangesAsync();

        return Ok(new { Message = "Huỷ lịch thành công" });
    }
}

public class CreateBookingDto
{
    public string CustomerName { get; set; } = string.Empty;
    public string CustomerPhone { get; set; } = string.Empty;
    public string? CustomerEmail { get; set; }
    public string? Notes { get; set; }
    public DateTime StartTime { get; set; }
    public List<int> ServiceIds { get; set; } = new();
}
