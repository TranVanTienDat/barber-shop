## Context

Hệ thống đặt lịch cắt tóc đang trong quá trình chuyển đổi từ dữ liệu tĩnh (Mock) sang dữ liệu thực tế (Real-world data). Hiện tại, trang `Admin/Services` đã hoàn thiện CRUD, cần áp dụng mô hình tương tự cho `Customers` và `Bookings`.

## Goals / Non-Goals

**Goals:**

- Kết nối `AdminController` với `ApplicationDbContext` để truy xuất thực tế dữ liệu khách hàng.
- Xây dựng hệ thống quản lý lịch đặt với các trạng thái rõ ràng (Confirmed, Cancelled, Completed).
- Đảm bảo giao diện đồng bộ với Stitch UI trong `Admin/Customers` và `Admin/Bookings`.

**Non-Goals:**

- Tự động gửi Email thông báo (Sẽ xử lý ở task riêng biệt sau).
- Hệ thống thanh toán trực tuyến.

## Decisions

- **Cấu trúc Dữ liệu**:
  - Khách hàng sẽ được ánh xạ từ bảng `AdminAccounts` (hoặc bảng độc lập nếu cần, nhưng hiện tại ưu tiên dùng bảng hiện có cho khách hàng để đơn giản hóa quản lý tài khoản).
  - Lịch đặt (`Booking`) sẽ liên kết với `BarberService` qua `ServiceId` và `AdminAccount` qua `CustomerId`.
- **Logic Trạng thái Lịch đặt**: Trạng thái sẽ là một Enum hoặc danh sách chuỗi cố định (Pending, Confirmed, Cancelled, Completed).
- **Frontend**: Sử dụng các Component (Table, Badge, Modal) của Stitch UI để hiển thị.

## Risks / Trade-offs

- **Security**: Đảm bảo các Action trong `AdminController` có thuộc tính `[Authorize(Roles = "Admin")]`.
- **Conflict**: Tránh xung đột lịch cắt tóc của cùng một nhân viên/vợt cắt (Validation logic sẽ đơn giản ở bước này, nâng cao sau).
