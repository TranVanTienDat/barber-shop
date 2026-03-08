using BarberBookingWeb.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using System.Text.Json;

namespace BarberBookingWeb.Data;

public class SeedDataFormat
{
    public List<CategoryData> Categories { get; set; } = new();
    public List<ServiceData> Services { get; set; } = new();
    public List<CustomerData> Customers { get; set; } = new();
}

public class CategoryData
{
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
}

public class ServiceData
{
    public string CategoryName { get; set; } = "";
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public int DurationInMinutes { get; set; }
    public decimal Price { get; set; }
}

public class CustomerData
{
    public string FullName { get; set; } = "";
    public string PhoneNumber { get; set; } = "";
    public string Email { get; set; } = "";
}

public static class DataSeeder
{
    public static void Initialize(ApplicationDbContext context)
    {
        // Thêm tài khoản admin nếu chưa có hoặc cập nhật lại mật khẩu admin123
        var admin = context.AdminAccounts.FirstOrDefault(a => a.Username == "admin");
        if (admin == null)
        {
            admin = new AdminAccount
            {
                Username = "admin",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                FullName = "Administrator",
                Role = "Admin",
                Email = "admin@barbershop.com"
            };
            context.AdminAccounts.Add(admin);
            context.SaveChanges();
            Console.WriteLine("--- CREATED DEFAULT ADMIN ACCOUNT ---");
        }
        else if (!BCrypt.Net.BCrypt.Verify("admin123", admin.PasswordHash))
        {
            admin.PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123");
            context.SaveChanges();
            Console.WriteLine("--- FIXED ADMIN PASSWORD ---");
        }

        // Check if DB already has been seeded with many items
        if (context.BarberServices.Count() >= 20)
        {
            return; // DB has been seeded with JSON data
        }

        Console.WriteLine("--- BẮT ĐẦU SEED DỮ LIỆU JSON THẬT ---");

        // Đọc dữ liệu từ file JSON
        var jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "seed_data.json");
        if (!File.Exists(jsonFilePath))
        {
            Console.WriteLine($"[LỖI] Không tìm thấy file seed_data.json tại: {jsonFilePath}");
            return;
        }

        var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var jsonString = File.ReadAllText(jsonFilePath);
        var seedData = JsonSerializer.Deserialize<SeedDataFormat>(jsonString, jsonOptions);

        if (seedData == null) return;

        // 1. Seed Categories if empty
        if (!context.Categories.Any() && seedData.Categories.Any())
        {
            foreach (var cat in seedData.Categories)
            {
                context.Categories.Add(new Category { Name = cat.Name, Description = cat.Description });
            }
            context.SaveChanges();
            Console.WriteLine("--- ĐÃ SEED DANH MỤC THẬT ---");
        }

        var dbCategories = context.Categories.ToList();

        // 2. Seed Services
        if (!context.BarberServices.Any(s => s.Name.StartsWith("Cắt tóc nam tiêu chuẩn")) && seedData.Services.Any())
        {
            var newServices = new List<BarberService>();
            foreach (var s in seedData.Services)
            {
                var cat = dbCategories.FirstOrDefault(c => c.Name == s.CategoryName);
                if (cat == null) continue;

                newServices.Add(new BarberService
                {
                    CategoryId = cat.Id,
                    CategoryName = cat.Name,
                    Name = s.Name,
                    Description = s.Description,
                    DurationInMinutes = s.DurationInMinutes,
                    Price = s.Price,
                    IsApproved = true
                });
            }
            context.BarberServices.AddRange(newServices);
            context.SaveChanges();
            Console.WriteLine($"--- ĐÃ THÊM {newServices.Count} DỊCH VỤ TỪ JSON ---");
        }

        // 3. Seed Customers
        var dbCustomers = context.AdminAccounts.Where(a => a.Role == "Customer").ToList();
        if (dbCustomers.Count < 20 && seedData.Customers.Any())
        {
            var newCustomers = new List<AdminAccount>();
            var passwordHash = BCrypt.Net.BCrypt.HashPassword("Khang123@#"); // Pass chung cho KH

            foreach (var c in seedData.Customers)
            {
                if (dbCustomers.Any(db => db.PhoneNumber == c.PhoneNumber)) continue;

                newCustomers.Add(new AdminAccount
                {
                    Username = c.PhoneNumber,
                    PasswordHash = passwordHash,
                    FullName = c.FullName,
                    PhoneNumber = c.PhoneNumber,
                    Email = c.Email,
                    Role = "Customer",
                    RegisteredDate = DateTime.UtcNow.AddDays(-new Random().Next(1, 60))
                });
            }
            context.AdminAccounts.AddRange(newCustomers);
            context.SaveChanges();

            // Cập nhật lại list customer
            dbCustomers.AddRange(newCustomers);
            Console.WriteLine($"--- ĐÃ THÊM KHÁCH HÀNG TỪ JSON ---");
        }

        // 4. Seed Bookings
        if (context.Bookings.Count() < 10)
        {
            var bookings = new List<Booking>();
            var allServices = context.BarberServices.ToList();
            var random = new Random();

            for (int i = 0; i < 30; i++) // Tạo 30 đơn
            {
                var customer = dbCustomers[random.Next(dbCustomers.Count)];

                var dateOffset = random.Next(-3, 5); // Tạo lịch hẹn từ -3 ngày đến +5 ngày 
                var hour = random.Next(9, 19); // Từ 9h - 18h
                var minute = random.Next(0, 2) == 0 ? 0 : 30;
                var startTime = DateTime.Today.AddDays(dateOffset).AddHours(hour).AddMinutes(minute);

                var numServices = random.Next(1, 3);
                var selectedServices = allServices.OrderBy(x => random.Next()).Take(numServices).ToList();
                int totalDuration = selectedServices.Sum(s => s.DurationInMinutes);

                // Make 80% Confirmed/Completed, 20% Pending/Canceled
                int statusRand = random.Next(100);
                BookingStatus status;
                if (statusRand < 50) status = BookingStatus.Confirmed;
                else if (statusRand < 80) status = BookingStatus.Completed;
                else if (statusRand < 90) status = BookingStatus.Pending;
                else status = BookingStatus.Cancelled;

                var booking = new Booking
                {
                    CustomerId = customer.Id,
                    CustomerName = customer.FullName ?? "Khách Lẻ",
                    CustomerPhone = customer.PhoneNumber ?? "",
                    CustomerEmail = customer.Email,
                    Notes = status == BookingStatus.Cancelled ? "Bận việc đột xuất." : "Yêu cầu thợ làm kĩ.",
                    StartTime = startTime,
                    EndTime = startTime.AddMinutes(totalDuration),
                    Status = status,
                    CancellationGuid = Guid.NewGuid()
                };

                foreach (var s in selectedServices)
                {
                    booking.BookingServices.Add(new BookingService { ServiceId = s.Id });
                }

                bookings.Add(booking);
            }
            context.Bookings.AddRange(bookings);
            context.SaveChanges();
            Console.WriteLine("--- ĐÃ SINH 30 LỊCH HẸN BẰNG DỮ LIỆU JSON ---");
        }
    }
}
