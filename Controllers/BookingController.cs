using Microsoft.AspNetCore.Mvc;
using BarberBookingWeb.Data;
using BarberBookingWeb.Models;
using BarberBookingWeb.Services;
using Microsoft.EntityFrameworkCore;

namespace BarberBookingWeb.Controllers;

public class BookingController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IEmailService _emailService;

    public BookingController(ApplicationDbContext context, IEmailService emailService)
    {
        _context = context;
        _emailService = emailService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var services = await _context.BarberServices
            .Include(s => s.Category)
            .Where(s => s.IsApproved)
            .ToListAsync();

        ViewBag.Categories = await _context.Categories.ToListAsync();

        return View(services);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Confirm(
        string customerName,
        string customerPhone,
        string customerEmail,
        string notes,
        string bookingDate,
        string bookingTime,
        List<int> selectedServices)
    {
        if (!DateTime.TryParse($"{bookingDate} {bookingTime}", out var startTime))
        {
            return BadRequest("Ngày giờ không hợp lệ.");
        }

        if (selectedServices == null || !selectedServices.Any())
        {
            return BadRequest("Vui lòng chọn ít nhất một dịch vụ.");
        }

        // 3.2 Server-side validation
        var services = await _context.BarberServices.Where(s => selectedServices.Contains(s.Id)).ToListAsync();
        int totalMinutes = services.Sum(s => s.DurationInMinutes);
        var endTime = startTime.AddMinutes(totalMinutes);

        var isOverlapping = await _context.Bookings.AnyAsync(b =>
            b.Status != BookingStatus.Cancelled &&
            ((startTime < b.EndTime) && (endTime > b.StartTime))
        );

        if (isOverlapping)
        {
            // Trùng lịch
            return Content("Xin lỗi, khung giờ này đã có khách đặt hoặc bị chồng chéo với lịch khác. Vui lòng quay lại và chọn giờ khác.");
        }

        var booking = new Booking
        {
            CustomerName = customerName,
            CustomerPhone = customerPhone,
            CustomerEmail = customerEmail,
            Notes = notes,
            StartTime = startTime,
            EndTime = startTime.AddMinutes(totalMinutes),
            Status = BookingStatus.Pending,
            CancellationGuid = Guid.NewGuid()
        };

        // 3.3 Many-to-many relationship
        foreach (var service in services)
        {
            booking.BookingServices.Add(new BookingService { ServiceId = service.Id });
        }

        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync();

        // 4.1 Send Email - Notify Pending
        if (!string.IsNullOrEmpty(customerEmail))
        {
            var cancelLink = Url.Action("Cancel", "Booking", new { id = booking.CancellationGuid }, Request.Scheme);
            string emailBody = $@"
<div style='font-family: Arial, sans-serif; line-height: 1.6; color: #333; max-width: 600px; margin: 0 auto; border: 1px solid #eee; border-radius: 10px; overflow: hidden;'>
    <div style='background-color: #19e680; padding: 20px; text-align: center;'>
        <h2 style='color: #112119; margin: 0;'>Yêu Cầu Đang Chờ Duyệt</h2>
    </div>
    <div style='padding: 30px;'>
        <p>Xin chào <strong>{customerName}</strong>,</p>
        <p>Yêu cầu đặt lịch làm đẹp của bạn tại <strong>Salon Làm Đẹp</strong> đã được hệ thống tiếp nhận.</p>
        <div style='background-color: #f9f9f9; padding: 20px; border-radius: 10px; margin: 20px 0;'>
            <p style='margin: 0 0 10px 0;'><strong>Chi tiết dự kiến:</strong></p>
            <ul style='list-style: none; padding: 0; margin: 0;'>
                <li style='margin-bottom: 5px;'>📅 <strong>Thời gian:</strong> {startTime.ToString("dd/MM/yyyy HH:mm")}</li>
                <li style='margin-bottom: 5px;'>✂️ <strong>Dịch vụ:</strong> {string.Join(", ", services.Select(s => s.Name))}</li>
            </ul>
        </div>
        <p>Chúng tôi sẽ xử lý và gửi email xác nhận cho bạn trong thời gian sớm nhất.</p>
        <hr style='border: 0; border-top: 1px solid #eee; margin: 20px 0;' />
        <p style='font-size: 0.85em; color: #777;'>Nếu bạn muốn thay đổi hoặc hủy yêu cầu này, vui lòng sử dụng liên kết bên dưới:</p>
        <div style='text-align: center; margin: 20px 0;'>
            <a href='{cancelLink}' style='background-color: #f44336; color: white; padding: 10px 20px; text-decoration: none; border-radius: 5px; font-weight: bold;'>Hủy yêu cầu đặt lịch</a>
        </div>
        <p style='font-size: 0.8em; color: #999; text-align: center;'>Cảm ơn bạn đã tin tưởng dịch vụ của chúng tôi.</p>
    </div>
</div>
            ";

            await _emailService.SendEmailAsync(customerEmail, "Yêu cầu đặt lịch tại Salon đang chờ duyệt", emailBody);
        }

        return RedirectToAction("Success");
    }

    [HttpGet]
    public IActionResult Success()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Cancel(Guid id)
    {
        var booking = await _context.Bookings
            .Include(b => b.BookingServices)
            .ThenInclude(bs => bs.BarberService)
            .FirstOrDefaultAsync(b => b.CancellationGuid == id);

        if (booking == null)
        {
            ViewBag.Message = "Link hủy lịch không hợp lệ hoặc không tồn tại.";
            ViewBag.IsSuccess = false;
            return View();
        }

        if (booking.Status == BookingStatus.Cancelled)
        {
            ViewBag.Message = "Lịch đặt này đã được hủy trước đó.";
            ViewBag.IsSuccess = false;
            return View(booking);
        }

        return View(booking);
    }

    [HttpPost]
    public async Task<IActionResult> DoCancel(Guid id)
    {
        var booking = await _context.Bookings.FirstOrDefaultAsync(b => b.CancellationGuid == id);
        if (booking == null)
        {
            return Json(new { success = false, message = "Link hủy lịch không hợp lệ hoặc không tồn tại." });
        }

        if (booking.Status == BookingStatus.Cancelled)
        {
            return Json(new { success = false, message = "Lịch đặt này đã được hủy trước đó." });
        }

        booking.Status = BookingStatus.Cancelled;
        await _context.SaveChangesAsync();

        // Gửi email thông báo hủy thành công
        if (!string.IsNullOrEmpty(booking.CustomerEmail))
        {
            string emailBody = $@"
Xin chào {booking.CustomerName},

Lịch hẹn của bạn vào lúc {booking.StartTime.ToString("dd/MM/yyyy HH:mm")} đã được hủy thành công theo yêu cầu.

Chúng tôi rất tiếc vì không được phục vụ bạn lần này. Hẹn gặp lại bạn vào dịp khác!

Trân trọng,
Đội ngũ Barber.
            ";
            await _emailService.SendEmailAsync(booking.CustomerEmail, "Đã hủy lịch làm đẹp thành công", emailBody);
        }

        return Json(new { success = true, message = "Hủy lịch hẹn thành công." });
    }

    [HttpGet("api/bookings/available-slots")]
    public async Task<IActionResult> GetAvailableSlots([FromQuery] DateTime date)
    {
        // Simple mock logic for available slots from 8:30 to 20:30
        var slots = new List<object>();
        var startHour = new TimeSpan(8, 30, 0);
        var endHour = new TimeSpan(20, 30, 0);
        var interval = TimeSpan.FromMinutes(30);

        var targetDate = date.Date;
        var existingBookings = await _context.Bookings
            .Where(b => b.Status != BookingStatus.Cancelled)
            .ToListAsync();

        // Filter in-memory to avoid potential SQLite date translation issues
        existingBookings = existingBookings.Where(b => b.StartTime.Date == targetDate).ToList();

        for (var time = startHour; time < endHour; time += interval)
        {
            var slotTime = date.Date.Add(time);

            // Check if any booking overlaps with this 30-minute slot
            bool isAvailable = !existingBookings.Any(b =>
                slotTime < b.EndTime && slotTime.AddMinutes(30) > b.StartTime
            );

            // Skip past slots for today
            if (slotTime < DateTime.Now)
            {
                isAvailable = false;
            }

            slots.Add(new
            {
                time = time.ToString(@"hh\:mm"),
                isAvailable = isAvailable
            });
        }

        return Json(slots);
    }
}
