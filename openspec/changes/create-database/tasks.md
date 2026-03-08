## 1. Domain Models Definition

- [x] 1.1 Tạo class `BarberService` với đầy đủ các thuộc tính (Id, Name, Price, v.v.).
- [x] 1.2 Tạo class `Booking` sử dụng Guid làm khóa chính.
- [x] 1.3 Tạo class trung gian `BookingService` để hỗ trợ quan hệ Many-to-Many.
- [x] 1.4 Tạo class `AdminAccount` cho mục đích xác thực quản trị.

## 2. Infrastructure & DbContext Setup

- [x] 2.1 Cài đặt NuGet package `Microsoft.EntityFrameworkCore.Sqlite`.
- [x] 2.2 Tạo class `ApplicationDbContext` và đăng ký các `DbSet`.
- [x] 2.3 Cấu hình Fluent API trong `OnModelCreating` (quan hệ N-N cho `BookingService`).
- [x] 2.4 Đăng ký `DbContext` vào `Program.cs` cấu hình dùng `UseSqlite`.

## 3. Migrations & Database Initialization

- [ ] 3.1 Tạo bản Migration đầu tiên: `AddInitialSchema`.
- [x] 3.2 Viết logic Seeding dữ liệu mẫu cho `BarberService` và `AdminAccount`.
- [x] 3.3 Tự động thực hiện `context.Database.Migrate()` khi khởi động app.

## 4. Verification

- [ ] 4.1 Kiểm tra file `barber.db` được tạo ra trong thư mục gốc.
- [ ] 4.2 Sử dụng công cụ (như SQLite Browser) để xác minh dữ liệu mẫu đã được nạp.
