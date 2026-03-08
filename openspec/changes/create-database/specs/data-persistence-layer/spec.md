## ADDED Requirements

### Requirement: DbContext Configuration

Hệ thống SHALL sử dụng `ApplicationDbContext` kế thừa từ `DbContext` của EF Core để quản lý các thực thể.

#### Scenario: Verify DbContext mapping

- **WHEN** khởi chạy ứng dụng
- **THEN** ApplicationDbContext phải đăng ký đầy đủ các DbSet: BarberServices, Bookings, BookingServices, và AdminAccounts.

### Requirement: Fluent API Relationship Mapping

Hệ thống SHALL sử dụng Fluent API để định cấu hình các mối quan hệ phức tạp, đặc biệt là quan hệ Nhiều-Nhiều.

#### Scenario: Verify multi-key mapping

- **WHEN** cấu hình OnModelCreating
- **THEN** BookingServices phải được định cấu hình Composite Key bao gồm (BookingId, ServiceId).

### Requirement: Connection String Management

Hệ thống SHALL sử dụng Connection String từ file cấu hình `appsettings.json` để kết nối tới SQL Server.

#### Scenario: Verify connection string usage

- **WHEN** ứng dụng khởi động trong môi trường Development
- **THEN** hệ thống phải đọc Connection String "DefaultConnection" trỏ tới file SQLite cục bộ (ví dụ: "Data Source=barber.db").

### Requirement: Migration Support

Hệ thống SHALL hỗ trợ tạo và áp dụng các bản vá (Migrations) vào cơ sở dữ liệu.

#### Scenario: Apply migration on startup

- **WHEN** ứng dụng khởi chạy (trong mode dev)
- **THEN** hệ thống nên tự động kiểm tra và áp dụng các Pending Migrations thông qua `context.Database.Migrate()`.
