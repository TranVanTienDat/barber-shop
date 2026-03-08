## Context

Hiện tại, trang quản lý dịch vụ (`Admin/Services.cshtml`) đang sử dụng dữ liệu mẫu (hardcoded). Hệ thống đã có Model `BarberService` và `ApplicationDbContext` nhưng chưa có các hàm xử lý (Action Methods) trong `AdminController` để thực hiện các thao tác CRUD.

## Goals / Non-Goals

**Goals:**

- Triển khai đầy đủ các tính năng: Xem danh sách, Thêm mới, Chỉnh sửa, và Xóa dịch vụ.
- Cập nhật giao diện `Admin/Services.cshtml` để sử dụng dữ liệu thật từ SQLite.
- Đảm bảo trải nghiệm người dùng mượt mà với các phản hồi (notifications) sau khi thao tác thành công.
- Tận dụng tối đa thiết kế Stitch UI đã xây dựng.

**Non-Goals:**

- Thay đổi cấu trúc cơ sở dữ liệu (Database Schema).
- Triển khai hệ thống phân quyền phức tạp (đã có [Authorize(Roles = "Admin")]).
- Tích hợp tải ảnh lên Cloud (tạm thời sử dụng ImageUrl dạng text).

## Decisions

- **Kiến trúc**: Sử dụng mô hình MVC truyền thống. Các Action sẽ nhận dữ liệu từ Form và thực hiện lưu trữ qua Entity Framework Core.
- **UI Interaction**:
  - Sử dụng trang riêng cho việc Thêm/Sửa để tận dụng không gian nhập liệu (Hoặc Modal nếu Lão đại yêu cầu đơn giản hơn, nhưng ở đây ưu tiên tính chuyên nghiệp của Stitch).
  - Sử dụng Confirmation Dialog (Hộp thoại xác nhận) trước khi thực hiện xóa dịch vụ.
- **Dữ liệu**: Truyền Model `IEnumerable<BarberService>` từ Controller sang View.
- **Thông báo**: Sử dụng `TempData` để hiển thị thông báo thành công/thất bại sau mỗi thao tác.

## Risks / Trade-offs

- **Image Management**: Việc chỉ nhập URL ảnh có thể gây khó khăn cho người dùng không am hiểu kỹ thuật. (Trade-off: Đơn giản hóa việc lưu trữ vào thời điểm hiện tại).
- **Concurrency**: Chưa xử lý xung đột nếu hai Admin cùng sửa một dịch vụ. (Risk: Thấp đối với quy mô tiệm tóc nhỏ).
