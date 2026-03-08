## Why

Hiện tại, hệ thống đặt lịch cắt tóc đã có khung giao diện Admin nhưng phần lớn dữ liệu vẫn là tĩnh (mock data), đặc biệt là quản lý Khách hàng và chưa có chức năng quản lý Lịch đặt (Bookings). Cần triển khai đầy đủ các thao tác CRUD (Thêm, Sửa, Xóa, Liệt kê) để chủ tiệm có thể vận hành thực tế, theo dõi lịch hẹn và thông tin khách hàng.

## What Changes

- **Quản lý Khách hàng (Customer Management)**:
  - Kết nối dữ liệu thật từ bảng `AdminAccounts` (hoặc tạo bảng `Customers` riêng nếu cần, nhưng hiện tại đề xuất dùng bảng `AdminAccount` cho người dùng hoặc mở rộng schema).
  - Cho phép Admin xem danh sách, thêm khách mới, sửa thông tin liên hệ và xóa khách hàng.
- **Quản lý Lịch đặt (Booking Management)**:
  - Xây dựng giao diện danh sách lịch đặt (Table/Calendar View).
  - Cho phép Admin tạo lịch đặt thủ công cho khách.
  - Cho phép chỉnh sửa trạng thái lịch đặt (Chờ duyệt, Đã xác nhận, Đã hoàn thành, Đã hủy).
  - Xóa các lịch đặt sai hoặc không hợp lệ.
- **Cập nhật Backend**:
  - Bổ sung các Action tương ứng vào `AdminController`.
  - Đảm bảo tính nhất quán dữ liệu (Validation) khi thao tác với Database.

## Capabilities

### New Capabilities

- `booking-management`: Xử lý toàn bộ các thao tác CRUD liên quan đến lịch đặt chỗ (Bookings).
- `customer-management`: Xử lý các thao tác quản lý thông tin khách hàng (Customers).

### Modified Capabilities

- `service-management`: Có thể cần điều chỉnh nhỏ để hiển thị thông tin dịch vụ trong luồng tạo Booking.

## Impact

- **Models**: `Booking`, `BarberService`, `AdminAccount`.
- **Controllers**: `AdminController` (Thêm các Action: Customers, CreateCustomer, EditCustomer, DeleteCustomer, Bookings, CreateBooking, EditBooking, DeleteBooking).
- **Views**: Cập nhật `Views/Admin/Customers.cshtml`, tạo mới `Views/Admin/Bookings.cshtml`, `CreateBooking.cshtml`, `EditBooking.cshtml`.
- **Database**: Thực hiện các truy vấn LINQ/EF Core trên SQLite.
