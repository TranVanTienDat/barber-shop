## Why

Dự án cần một nền tảng lưu trữ dữ liệu bền vững để quản lý các dịch vụ cắt tóc, thông tin đặt lịch của khách hàng và xác thực quyền truy cập của quản trị viên theo thiết kế đã thống nhất trên Stitch. Việc thiết lập database schema ngay từ đầu giúp đảm bảo tính toàn vẹn dữ liệu và hỗ trợ tính năng cho phép khách hàng chọn đồng thời nhiều dịch vụ trong một lần đặt lịch (Many-to-Many).

## What Changes

- Thiết lập cấu trúc Database Schema (SQLite) phù hợp với yêu cầu nghiệp vụ.
- Định nghĩa các thực thể (Entities) trong mã nguồn bao gồm: `BarberService`, `Booking`, `BookingService` (Join table), và `AdminAccount`.
- Cấu hình Entity Framework Core (EF Core) để ánh xạ các thực thể vào SQLite database.
- Tích hợp khả năng tự động khởi tạo cơ sở dữ liệu và dữ liệu mẫu (Seeding) dựa trên các dịch vụ thực tế trong thiết kế.

## Capabilities

### New Capabilities

- `database-schema-definition`: Định nghĩa chi tiết cấu trúc các bảng và mối quan hệ (1-N cho Admin-Services, N-N cho Booking-Services).
- `data-persistence-layer`: Cấu hình `ApplicationDbContext` và các cấu hình Fluent API cần thiết để quản lý file dữ liệu SQLite cục bộ.
- `initial-data-seeding`: Cung cấp dữ liệu mẫu cho các dịch vụ cắt tóc (Cắt tóc, Gội đầu, Nhuộm, v.v.) để hiển thị ngay trên giao diện khách hàng.

### Modified Capabilities

- (Không có - Đây là giai đoạn khởi tạo nền móng)

## Impact

- **Database Layer**: Tạo mới file cơ sở dữ liệu SQLite (`barber.db`).
- **Domain Layer**: Bổ sung các Model classes trong project chính.
- **Infrastructure Layer**: Thiết lập EF Core Migrations.
- **Security**: Cấu hình bảng Admin để phục vụ tính năng Đăng nhập quản trị viên.
