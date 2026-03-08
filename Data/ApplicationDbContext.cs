using Microsoft.EntityFrameworkCore;
using BarberBookingWeb.Models;

namespace BarberBookingWeb.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<BarberService> BarberServices { get; set; } = null!;
    public DbSet<Booking> Bookings { get; set; } = null!;
    public DbSet<BookingService> BookingServices { get; set; } = null!;
    public DbSet<AdminAccount> AdminAccounts { get; set; } = null!;
    public DbSet<AppSetting> AppSettings { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Category - BarberService (1-N)
        modelBuilder.Entity<BarberService>()
            .HasOne(s => s.Category)
            .WithMany(c => c.BarberServices)
            .HasForeignKey(s => s.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure Many-to-Many Relationship via BookingService
        modelBuilder.Entity<BookingService>()
            .HasKey(bs => new { bs.BookingId, bs.ServiceId });

        modelBuilder.Entity<BookingService>()
            .HasOne(bs => bs.Booking)
            .WithMany(b => b.BookingServices)
            .HasForeignKey(bs => bs.BookingId);

        modelBuilder.Entity<BookingService>()
            .HasOne(bs => bs.BarberService)
            .WithMany(s => s.BookingServices)
            .HasForeignKey(bs => bs.ServiceId);

        // Configure Decimal for SQLite (Mapped as TEXT/REAL by default, decimal in code)
        // Note: For SQLite, decimal to string conversion is often needed for precise sorting, 
        // but EF Core handles basic decimal usage well.

        // Additional configurations from Screen requirements (e.g. Unique Admin username)
        modelBuilder.Entity<AdminAccount>()
            .HasIndex(a => a.Username)
            .IsUnique();

        // Seed Categories
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Cắt tóc", Description = "Các dịch vụ cắt tóc nam" },
            new Category { Id = 2, Name = "Combo gội sấy", Description = "Dịch vụ combo bao gồm gội và sấy" },
            new Category { Id = 3, Name = "Cạo mặt", Description = "Dịch vụ cạo mặt truyền thống" },
            new Category { Id = 4, Name = "Combo VIP", Description = "Các gói dịch vụ cao cấp" }
        );

        // Seed initial BarberServices
        modelBuilder.Entity<BarberService>().HasData(
            new BarberService { Id = 1, CategoryId = 1, CategoryName = "Cắt tóc", Name = "Cắt tóc nam", Description = "Cắt tóc nam phong cách hiện đại", DurationInMinutes = 30, Price = 80000, IsApproved = true },
            new BarberService { Id = 2, CategoryId = 1, CategoryName = "Cắt tóc", Name = "Cắt tóc & Gội đầu", Description = "Combo cắt tóc và gội đầu thư giãn", DurationInMinutes = 45, Price = 120000, IsApproved = true },
            new BarberService { Id = 3, CategoryId = 4, CategoryName = "Combo VIP", Name = "Combo VIP 7 bước", Description = "Trải nghiệm đầy đủ các dịch vụ chăm sóc râu tóc", DurationInMinutes = 60, Price = 250000, IsApproved = true },
            new BarberService { Id = 4, CategoryId = 3, CategoryName = "Cạo mặt", Name = "Cạo mặt truyền thống", Description = "Cạo mặt với khăn nóng và dao cạo truyền thống", DurationInMinutes = 15, Price = 50000, IsApproved = true }
        );

        // Seed initial AdminAccount (Password: admin123 - BCrypt Hash)
        modelBuilder.Entity<AdminAccount>().HasData(
            new AdminAccount
            {
                Id = 1,
                Username = "admin",
                PasswordHash = "$2a$11$N9qo8uLOickgx2ZMRZoMyeIjZAgNoT6vV./L5u.03z.N/hS5n2762",
                FullName = "Hệ thống Admin",
                Role = "Admin",
                RegisteredDate = new DateTime(2025, 3, 5, 0, 0, 0, DateTimeKind.Utc)
            },
            new AdminAccount
            {
                Id = 2,
                Username = "customer1",
                PasswordHash = "$2a$11$N9qo8uLOickgx2ZMRZoMyeIjZAgNoT6vV./L5u.03z.N/hS5n2762",
                FullName = "Nguyễn Văn A",
                PhoneNumber = "0901234567",
                Email = "vana@example.com",
                Role = "Customer",
                RegisteredDate = new DateTime(2025, 3, 5, 10, 0, 0, DateTimeKind.Utc)
            },
            new AdminAccount
            {
                Id = 3,
                Username = "customer2",
                PasswordHash = "$2a$11$N9qo8uLOickgx2ZMRZoMyeIjZAgNoT6vV./L5u.03z.N/hS5n2762",
                FullName = "Trần Thị B",
                PhoneNumber = "0987654321",
                Email = "thib@example.com",
                Role = "Customer",
                RegisteredDate = new DateTime(2025, 3, 6, 11, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}
