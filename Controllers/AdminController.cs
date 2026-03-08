using BarberBookingWeb.Data;
using BarberBookingWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BarberBookingWeb.Services;
using ClosedXML.Excel;
using System.IO;

namespace BarberBookingWeb.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _env;
    private readonly IEmailService _emailService;

    public AdminController(ApplicationDbContext context, IWebHostEnvironment env, IEmailService emailService)
    {
        _context = context;
        _env = env;
        _emailService = emailService;
    }

    public IActionResult Index()
    {
        return RedirectToAction(nameof(Bookings));
    }

    public async Task<IActionResult> Services(string search, int? categoryId, int? minPrice, int? maxPrice, int page = 1, int pageSize = 10)
    {
        var query = _context.BarberServices.Include(s => s.Category).AsQueryable();

        if (!string.IsNullOrEmpty(search))
        {
            var searchLower = search.ToLower();
            query = query.Where(s => (s.Name != null && s.Name.ToLower().Contains(searchLower)) ||
                                     (s.Category != null && s.Category.Name.ToLower().Contains(searchLower)) ||
                                     (s.CategoryName != null && s.CategoryName.ToLower().Contains(searchLower)));
        }

        if (minPrice.HasValue)
            query = query.Where(s => s.Price >= minPrice.Value);

        if (maxPrice.HasValue)
            query = query.Where(s => s.Price <= maxPrice.Value);

        if (categoryId.HasValue)
            query = query.Where(s => s.CategoryId == categoryId.Value);

        var total = await query.CountAsync();
        var services = await query.OrderBy(s => s.Name)
            .Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        ViewBag.Search = search;
        ViewBag.MinPrice = minPrice;
        ViewBag.MaxPrice = maxPrice;
        ViewBag.CategoryId = categoryId;
        ViewBag.Categories = await _context.Categories.ToListAsync();

        return View(new PagedResult<BarberService> { Items = services, TotalCount = total, CurrentPage = page, PageSize = pageSize });
    }

    [HttpGet]
    public async Task<IActionResult> CreateService()
    {
        ViewBag.Categories = await _context.Categories.ToListAsync();
        return View(new BarberService());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateService(BarberService service, IFormFile? imageFile)
    {
        ModelState.Remove("imageFile");
        if (ModelState.IsValid)
        {
            try
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    var uploadsDir = Path.Combine(_env.WebRootPath, "images", "services");
                    if (!Directory.Exists(uploadsDir)) Directory.CreateDirectory(uploadsDir);

                    var ext = Path.GetExtension(imageFile.FileName);
                    var fileName = $"{Guid.NewGuid()}{ext}";
                    var filePath = Path.Combine(uploadsDir, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }
                    service.ImageUrl = $"/images/services/{fileName}";
                }

                _context.Add(service);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Thêm dịch vụ thành công!";
                return RedirectToAction(nameof(Services));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Lỗi khi lưu ảnh: " + ex.Message);
            }
        }
        ViewBag.Categories = await _context.Categories.ToListAsync();
        return View(service);
    }

    [HttpGet]
    public async Task<IActionResult> EditService(int id)
    {
        var service = await _context.BarberServices.FindAsync(id);
        if (service == null) return NotFound();
        ViewBag.Categories = await _context.Categories.ToListAsync();
        return View(service);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditService(int id, BarberService service, IFormFile? imageFile)
    {
        if (id != service.Id) return NotFound();

        ModelState.Remove("imageFile");
        if (ModelState.IsValid)
        {
            try
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    var uploadsDir = Path.Combine(_env.WebRootPath, "images", "services");
                    Directory.CreateDirectory(uploadsDir);
                    var ext = Path.GetExtension(imageFile.FileName);
                    var fileName = $"{Guid.NewGuid()}{ext}";
                    var filePath = Path.Combine(uploadsDir, fileName);
                    using var stream = new FileStream(filePath, FileMode.Create);
                    await imageFile.CopyToAsync(stream);
                    service.ImageUrl = $"/images/services/{fileName}";
                }
                _context.Update(service);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Cập nhật dịch vụ thành công!";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.BarberServices.AnyAsync(e => e.Id == service.Id)) return NotFound();
                else throw;
            }
            return RedirectToAction(nameof(Services));
        }
        ViewBag.Categories = await _context.Categories.ToListAsync();
        return View(service);
    }

    public async Task<IActionResult> DeleteService(int id)
    {
        var service = await _context.BarberServices.FindAsync(id);
        if (service != null)
        {
            _context.BarberServices.Remove(service);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Xóa dịch vụ thành công!";
        }
        return RedirectToAction(nameof(Services));
    }

    #region Categories Management

    public async Task<IActionResult> Categories(string search, int page = 1, int pageSize = 10)
    {
        var query = _context.Categories.AsQueryable();

        if (!string.IsNullOrEmpty(search))
        {
            var searchLower = search.ToLower();
            query = query.Where(c => c.Name.ToLower().Contains(searchLower) || (c.Description != null && c.Description.ToLower().Contains(searchLower)));
        }

        var total = await query.CountAsync();
        var categories = await query.OrderBy(c => c.Name)
            .Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        ViewBag.Search = search;

        return View(new PagedResult<Category> { Items = categories, TotalCount = total, CurrentPage = page, PageSize = pageSize });
    }

    [HttpGet]
    public IActionResult CreateCategory()
    {
        return View(new Category());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateCategory(Category category)
    {
        if (ModelState.IsValid)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Thêm danh mục thành công!";
            return RedirectToAction(nameof(Categories));
        }
        return View(category);
    }

    [HttpGet]
    public async Task<IActionResult> EditCategory(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null) return NotFound();
        return View(category);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditCategory(int id, Category category)
    {
        if (id != category.Id) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(category);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Cập nhật danh mục thành công!";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Categories.AnyAsync(e => e.Id == category.Id)) return NotFound();
                else throw;
            }
            return RedirectToAction(nameof(Categories));
        }
        return View(category);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var category = await _context.Categories.Include(c => c.BarberServices).FirstOrDefaultAsync(c => c.Id == id);
        if (category == null) return NotFound();

        if (category.BarberServices.Any())
        {
            TempData["ErrorMessage"] = "Không thể xóa danh mục này vì đang có dịch vụ liên kết!";
        }
        else
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Xóa danh mục thành công!";
        }
        return RedirectToAction(nameof(Categories));
    }

    #endregion


    public async Task<IActionResult> Customers(string search, int page = 1, int pageSize = 10)
    {
        var query = _context.AdminAccounts.Where(a => a.Role == "Customer").AsQueryable();

        if (!string.IsNullOrEmpty(search))
        {
            var searchLower = search.ToLower();
            query = query.Where(c => (c.FullName != null && c.FullName.ToLower().Contains(searchLower)) || (c.PhoneNumber != null && c.PhoneNumber.Contains(searchLower)) || (c.Email != null && c.Email.ToLower().Contains(searchLower)));
        }

        var total = await query.CountAsync();
        var customers = await query.OrderByDescending(a => a.RegisteredDate)
            .Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        ViewBag.Search = search;

        return View(new PagedResult<AdminAccount> { Items = customers, TotalCount = total, CurrentPage = page, PageSize = pageSize });
    }

    [HttpGet]
    public IActionResult CreateCustomer()
    {
        return View(new AdminAccount { Role = "Customer" });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateCustomer(AdminAccount customer)
    {
        ModelState.Remove("PasswordHash");
        ModelState.Remove("RegisteredDate");
        if (ModelState.IsValid)
        {
            try
            {
                // Check if username already exists
                if (await _context.AdminAccounts.AnyAsync(a => a.Username == customer.Username))
                {
                    ModelState.AddModelError("Username", "Tên đăng nhập này đã tồn tại vui lòng chọn tên khác.");
                    return View(customer);
                }

                customer.Role = "Customer";
                customer.RegisteredDate = DateTime.Now;
                if (string.IsNullOrEmpty(customer.PasswordHash))
                {
                    customer.PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456");
                }
                _context.Add(customer);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Thêm khách hàng thành công!";
                return RedirectToAction(nameof(Customers));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Có lỗi xảy ra khi lưu database: " + ex.Message);
            }
        }
        return View(customer);
    }

    [HttpGet]
    public async Task<IActionResult> EditCustomer(int id)
    {
        var customer = await _context.AdminAccounts.FindAsync(id);
        if (customer == null || customer.Role != "Customer") return NotFound();
        return View(customer);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditCustomer(int id, AdminAccount customer)
    {
        if (id != customer.Id) return NotFound();

        ModelState.Remove("PasswordHash");
        ModelState.Remove("RegisteredDate");
        if (ModelState.IsValid)
        {
            try
            {
                var existing = await _context.AdminAccounts.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
                if (existing == null) return NotFound();

                // Check if new username belongs to someone else
                if (await _context.AdminAccounts.AnyAsync(a => a.Username == customer.Username && a.Id != id))
                {
                    ModelState.AddModelError("Username", "Tên đăng nhập này đã được sử dụng bởi người khác.");
                    return View(customer);
                }

                customer.Role = "Customer";
                customer.PasswordHash = existing.PasswordHash; // Keep existing password for now
                customer.RegisteredDate = existing.RegisteredDate;

                _context.Update(customer);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Cập nhật khách hàng thành công!";
                return RedirectToAction(nameof(Customers));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Có lỗi xảy ra: " + ex.Message);
            }
        }
        return View(customer);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteCustomer(int id)
    {
        var customer = await _context.AdminAccounts.FindAsync(id);
        if (customer != null && customer.Role == "Customer")
        {
            _context.AdminAccounts.Remove(customer);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Xóa khách hàng thành công!";
        }
        return RedirectToAction(nameof(Customers));
    }

    [HttpGet]
    public async Task<IActionResult> Bookings(string search, DateTime? startDate, DateTime? endDate, BookingStatus? status, int page = 1, int pageSize = 10)
    {
        var query = _context.Bookings
            .Include(b => b.Customer)
            .Include(b => b.BookingServices)
                .ThenInclude(bs => bs.BarberService)
            .AsQueryable();

        if (!string.IsNullOrEmpty(search))
        {
            var searchLower = search.ToLower();
            query = query.Where(b => (b.CustomerName != null && b.CustomerName.ToLower().Contains(searchLower)) ||
                                     (b.CustomerPhone != null && b.CustomerPhone.Contains(searchLower)) ||
                                     (b.CustomerEmail != null && b.CustomerEmail.ToLower().Contains(searchLower)));
        }

        if (startDate.HasValue)
            query = query.Where(b => b.StartTime >= startDate.Value);

        if (endDate.HasValue)
            query = query.Where(b => b.StartTime <= endDate.Value);

        if (status.HasValue)
            query = query.Where(b => b.Status == status.Value);

        var total = await query.CountAsync();
        var bookings = await query.OrderByDescending(b => b.StartTime)
            .Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        // Kiểm tra chồng chéo lịch để cảnh báo Admin
        if (bookings.Any())
        {
            var minStart = bookings.Min(b => b.StartTime);
            var maxEnd = bookings.Max(b => b.EndTime);

            // Lấy tất cả các lịch có khả năng chèn vào khoảng thời gian của trang hiện tại
            var potentialOverlaps = await _context.Bookings
                .Where(b => b.Status != BookingStatus.Cancelled && b.StartTime < maxEnd && b.EndTime > minStart)
                .ToListAsync();

            var overlappingIds = bookings
                .Where(b => potentialOverlaps.Any(other => other.Id != b.Id && (b.StartTime < other.EndTime && b.EndTime > other.StartTime)))
                .Select(b => b.Id)
                .ToList();

            ViewBag.OverlappingIds = overlappingIds;
        }

        ViewBag.Search = search;
        ViewBag.StartDate = startDate?.ToString("yyyy-MM-dd");
        ViewBag.EndDate = endDate?.ToString("yyyy-MM-dd");
        ViewBag.Status = status;

        return View(new PagedResult<Booking> { Items = bookings, TotalCount = total, CurrentPage = page, PageSize = pageSize });
    }

    [HttpGet]
    public async Task<IActionResult> ExportBookingsToExcel(string search, DateTime? startDate, DateTime? endDate, BookingStatus? status)
    {
        var query = _context.Bookings
            .Include(b => b.Customer)
            .Include(b => b.BookingServices)
                .ThenInclude(bs => bs.BarberService)
            .AsQueryable();

        if (!string.IsNullOrEmpty(search))
        {
            var searchLower = search.ToLower();
            query = query.Where(b => (b.CustomerName != null && b.CustomerName.ToLower().Contains(searchLower)) ||
                                     (b.CustomerPhone != null && b.CustomerPhone.Contains(searchLower)) ||
                                     (b.CustomerEmail != null && b.CustomerEmail.ToLower().Contains(searchLower)));
        }

        if (startDate.HasValue)
            query = query.Where(b => b.StartTime >= startDate.Value);

        if (endDate.HasValue)
            query = query.Where(b => b.StartTime <= endDate.Value);

        if (status.HasValue)
            query = query.Where(b => b.Status == status.Value);

        var bookings = await query.OrderByDescending(b => b.StartTime).ToListAsync();

        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("Danh sách đặt lịch");
            var currentRow = 1;

            // Header
            worksheet.Cell(currentRow, 1).Value = "STT";
            worksheet.Cell(currentRow, 2).Value = "Khách hàng";
            worksheet.Cell(currentRow, 3).Value = "Điện thoại";
            worksheet.Cell(currentRow, 4).Value = "Email";
            worksheet.Cell(currentRow, 5).Value = "Dịch vụ";
            worksheet.Cell(currentRow, 6).Value = "Ngày";
            worksheet.Cell(currentRow, 7).Value = "Bắt đầu";
            worksheet.Cell(currentRow, 8).Value = "Kết thúc";
            worksheet.Cell(currentRow, 9).Value = "Trạng thái";

            // Style Header
            var headerRow = worksheet.Row(1);
            headerRow.Style.Font.Bold = true;
            headerRow.Style.Fill.BackgroundColor = XLColor.LightGray;

            // Data
            int stt = 1;
            foreach (var booking in bookings)
            {
                currentRow++;
                worksheet.Cell(currentRow, 1).Value = stt++;
                worksheet.Cell(currentRow, 2).Value = booking.Customer?.FullName ?? booking.CustomerName;
                worksheet.Cell(currentRow, 3).Value = booking.CustomerPhone;
                worksheet.Cell(currentRow, 4).Value = booking.CustomerEmail;
                worksheet.Cell(currentRow, 5).Value = string.Join(", ", booking.BookingServices.Select(bs => bs.BarberService.Name));
                worksheet.Cell(currentRow, 6).Value = booking.StartTime.ToString("dd/MM/yyyy");
                worksheet.Cell(currentRow, 7).Value = booking.StartTime.ToString("HH:mm");
                worksheet.Cell(currentRow, 8).Value = booking.EndTime.ToString("HH:mm");

                string statusText = booking.Status switch
                {
                    BookingStatus.Pending => "Chờ duyệt",
                    BookingStatus.Confirmed => "Đã xác nhận",
                    BookingStatus.Completed => "Hoàn thành",
                    BookingStatus.Cancelled => "Đã hủy",
                    _ => booking.Status.ToString()
                };
                worksheet.Cell(currentRow, 9).Value = statusText;
            }

            worksheet.Columns().AdjustToContents();

            using (var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                var content = stream.ToArray();
                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"DanhSachDatLich_{DateTime.Now:yyyyMMddHHmm}.xlsx");
            }
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateBookingStatus(Guid id, BookingStatus status)
    {
        var booking = await _context.Bookings
            .Include(b => b.BookingServices)
                .ThenInclude(bs => bs.BarberService)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (booking == null) return NotFound();

        var oldStatus = booking.Status;
        booking.Status = status;
        await _context.SaveChangesAsync();

        // Gửi email nếu trạng thái chuyển sang Confirmed
        if (status == BookingStatus.Confirmed && oldStatus != BookingStatus.Confirmed && !string.IsNullOrEmpty(booking.CustomerEmail))
        {
            var services = string.Join(", ", booking.BookingServices.Select(bs => bs.BarberService.Name));
            var cancelLink = Url.Action("Cancel", "Booking", new { id = booking.CancellationGuid }, Request.Scheme);

            string emailBody = $@"
<div style='font-family: Arial, sans-serif; line-height: 1.6; color: #333;'>
    <h2 style='color: #2c3e50;'>Xác Nhận Lịch Hẹn Thành Công!</h2>
    <p>Xin chào <strong>{booking.CustomerName}</strong>,</p>
    <p>Lịch hẹn làm đẹp của bạn tại <strong>Salon Làm Đẹp</strong> đã được phê duyệt.</p>
    <hr />
    <p><strong>Chi tiết lịch hẹn:</strong></p>
    <ul>
        <li><strong>Thời gian:</strong> {booking.StartTime.ToString("dd/MM/yyyy HH:mm")}</li>
        <li><strong>Dịch vụ:</strong> {services}</li>
        <li><strong>Số điện thoại:</strong> {booking.CustomerPhone}</li>
    </ul>
    <p>Chúng tôi rất mong được đón tiếp bạn!</p>
    <hr />
    <p style='font-size: 0.9em;'>Nếu bạn muốn hủy lịch, vui lòng click vào link sau: <a href='{cancelLink}'>Hủy lịch hẹn</a></p>
    <p>Cảm ơn bạn đã tin tưởng dịch vụ của chúng tôi.</p>
</div>
            ";

            await _emailService.SendEmailAsync(booking.CustomerEmail, "Xác nhận lịch hẹn làm đẹp thành công", emailBody);
        }
        else if (status == BookingStatus.Cancelled && oldStatus != BookingStatus.Cancelled && !string.IsNullOrEmpty(booking.CustomerEmail))
        {
            string emailBody = $@"
<div style='font-family: Arial, sans-serif; line-height: 1.6; color: #333;'>
    <h2 style='color: #e74c3c;'>Thông Báo Hủy Lịch Hẹn</h2>
    <p>Xin chào <strong>{booking.CustomerName}</strong>,</p>
    <p>Chúng tôi rất tiếc phải thông báo rằng lịch hẹn làm đẹp của bạn vào lúc <strong>{booking.StartTime.ToString("dd/MM/yyyy HH:mm")}</strong> đã bị hủy bởi quản trị viên.</p>
    <p>Nếu có bất kỳ thắc mắc nào hoặc muốn đặt lại lịch, quý khách vui lòng liên hệ trực tiếp với chúng tôi hoặc thực hiện đặt lịch mới trên website.</p>
    <hr />
    <p>Rất mong được phục vụ quý khách vào dịp khác.</p>
    <p>Trân trọng!</p>
</div>
            ";

            await _emailService.SendEmailAsync(booking.CustomerEmail, "Thông báo hủy lịch làm đẹp", emailBody);
        }

        TempData["SuccessMessage"] = "Cập nhật trạng thái thành công!";
        return RedirectToAction(nameof(Bookings));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteBooking(Guid id)
    {
        var booking = await _context.Bookings.FindAsync(id);
        if (booking != null)
        {
            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Xóa lịch hẹn thành công!";
        }
        return RedirectToAction(nameof(Bookings));
    }

    [HttpGet]
    public async Task<IActionResult> CreateBooking()
    {
        ViewBag.Customers = await _context.AdminAccounts.Where(a => a.Role == "Customer").ToListAsync();
        ViewBag.Services = await _context.BarberServices.ToListAsync();
        return View(new Booking { StartTime = DateTime.Now, EndTime = DateTime.Now.AddHours(1) });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateBooking(Booking booking, List<int> serviceIds)
    {
        if (booking == null) return BadRequest();
        if (serviceIds == null || !serviceIds.Any())
        {
            ModelState.AddModelError("", "Vui lòng chọn ít nhất một dịch vụ.");
        }

        if (ModelState.IsValid)
        {
            // Tính toán EndTime dựa trên các dịch vụ đã chọn
            var services = await _context.BarberServices.Where(s => serviceIds.Contains(s.Id)).ToListAsync();
            int totalMinutes = services.Sum(s => s.DurationInMinutes);
            booking.EndTime = booking.StartTime.AddMinutes(totalMinutes);

            // Kiểm tra chồng chéo lịch
            var isOverlapping = await _context.Bookings.AnyAsync(b =>
                b.Status != BookingStatus.Cancelled &&
                ((booking.StartTime < b.EndTime) && (booking.EndTime > b.StartTime))
            );

            if (isOverlapping)
            {
                ModelState.AddModelError("", "Lỗi: Khung giờ này bị chồng chéo với một lịch hẹn khác đã có sẵn.");
            }
            else
            {
                booking.Id = Guid.NewGuid();
                booking.Status = BookingStatus.Confirmed;
                _context.Bookings.Add(booking);

                foreach (var sId in serviceIds)
                {
                    _context.BookingServices.Add(new BookingService
                    {
                        BookingId = booking.Id,
                        ServiceId = sId
                    });
                }

                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Tạo lịch hẹn thành công!";
                return RedirectToAction(nameof(Bookings));
            }
        }

        ViewBag.Customers = await _context.AdminAccounts.Where(a => a.Role == "Customer").ToListAsync();
        ViewBag.Services = await _context.BarberServices.ToListAsync();
        return View(booking);
    }

    [HttpGet]
    public async Task<IActionResult> EmailSettings()
    {
        var settings = await _context.AppSettings.ToListAsync();
        if (settings == null) settings = new List<AppSetting>();

        // Chuyển danh sách sang Dictionary để dễ hiển thị
        var model = new EmailSettings
        {
            SmtpServer = settings.FirstOrDefault(s => s.Key != null && s.Key == "Email_SmtpServer")?.Value ?? "smtp.gmail.com",
            Port = int.TryParse(settings.FirstOrDefault(s => s.Key != null && s.Key == "Email_Port")?.Value, out var p) ? p : 587,
            SenderName = settings.FirstOrDefault(s => s.Key != null && s.Key == "Email_SenderName")?.Value ?? "Barber Shop",
            SenderEmail = settings.FirstOrDefault(s => s.Key != null && s.Key == "Email_SenderEmail")?.Value ?? "",
            Username = settings.FirstOrDefault(s => s.Key != null && s.Key == "Email_Username")?.Value ?? "",
            Password = settings.FirstOrDefault(s => s.Key != null && s.Key == "Email_Password")?.Value ?? "",
            EnableSsl = settings.FirstOrDefault(s => s.Key != null && s.Key == "Email_EnableSsl")?.Value == "true"
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EmailSettings(EmailSettings model)
    {
        if (ModelState.IsValid)
        {
            var keys = new Dictionary<string, string>
            {
                { "Email_SmtpServer", model.SmtpServer },
                { "Email_Port", model.Port.ToString() },
                { "Email_SenderName", model.SenderName },
                { "Email_SenderEmail", model.SenderEmail },
                { "Email_Username", model.Username },
                { "Email_Password", model.Password },
                { "Email_EnableSsl", model.EnableSsl.ToString().ToLower() }
            };

            foreach (var item in keys)
            {
                var setting = await _context.AppSettings.FirstOrDefaultAsync(s => s.Key == item.Key);
                if (setting == null)
                {
                    _context.AppSettings.Add(new AppSetting { Key = item.Key, Value = item.Value });
                }
                else
                {
                    setting.Value = item.Value;
                }
            }

            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Cập nhật cấu hình Email thành công!";
            return RedirectToAction(nameof(EmailSettings));
        }

        return View(model);
    }

    public IActionResult Settings()
    {
        return RedirectToAction(nameof(EmailSettings));
    }
}
