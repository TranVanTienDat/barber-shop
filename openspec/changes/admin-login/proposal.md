## Why

Hiện tại hệ thống chưa có cơ chế bảo mật cho phần quản trị. Cần thiết lập chức năng đăng nhập để bảo vệ các dữ liệu nhạy cảm và cho phép Admin quản lý các dịch vụ và đơn đặt lịch. Việc sử dụng TailwindCSS giúp giao diện hiện đại và dễ tùy chỉnh hơn.

## What Changes

- **Thêm trang Đăng nhập (Admin Login):** Giao diện chuyên nghiệp dựa trên thiết kế Stitch.
- **Thiết lập Authentication Middleware:** Sử dụng Cookie Authentication để quản lý phiên làm việc của Admin.
- **Tích hợp TailwindCSS:** Cấu hình TailwindCSS vào dự án ASP.NET Core MVC làm framework CSS chính cho các trang Admin.
- **Admin Controller:** Xử lý logic đăng nhập, kiểm tra thông tin từ bảng `AdminAccounts` và đăng xuất.

## Capabilities

### New Capabilities

- `admin-auth`: Cung cấp khả năng xác thực người dùng, quản lý phiên làm việc bằng Cookie và bảo vệ các điều hướng yêu cầu quyền quản trị.
- `tailwind-integration`: Thiết lập môi trường phát triển TailwindCSS trong ASP.NET Core để sử dụng các utility class cho UI.

### Modified Capabilities

- `data-persistence-layer`: Cần đảm bảo bảng `AdminAccount` có đủ thông tin cho logic đăng nhập (hiện tại đã có Username và PasswordHash).

## Impact

- **Controllers:** Thêm `AccountController` hoặc cập nhật `AdminController`.
- **Views:** Thêm View cho trang Login.
- **Configuration:** Cập nhật `Program.cs` để đăng ký Authentication.
- **Asset Pipeline:** Thay đổi cách quản lý CSS để hỗ trợ Tailwind.
