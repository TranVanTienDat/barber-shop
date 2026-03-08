## 1. Backend: Cấu hình Controller & Database

- [x] 1.1 Cập nhật `AdminController.cs` để lấy danh sách khách hàng từ `AdminAccounts`.
- [x] 1.2 Triển khai các Action xử lý CRUD cho Khách hàng: `CreateCustomer`, `EditCustomer`, `DeleteCustomer`.
- [x] 1.3 Triển khai Action `Bookings` hỗ trợ `Include` cho `BarberService` và `Customer`.
- [x] 1.4 Triển khai các Action xử lý trạng thái Lịch đặt: `UpdateBookingStatus`, `DeleteBooking`.

## 2. Frontend: Cập nhật & Xây dựng Giao diện Admin

- [x] 2.1 Chuyển đổi `Admin/Customers.cshtml` từ dữ liệu tĩnh sang sử dụng `@IEnumerable<AdminAccount>`.
- [x] 2.2 Tạo mới trang `Admin/Bookings.cshtml` hiển thị danh sách lịch hẹn theo phong cách Stitch UI.
- [x] 2.3 Xây dựng các View phụ: `CreateBooking.cshtml` (nếu cần cho Admin tạo hộ khách).
- [x] 2.4 Tích hợp các nút bấm 'Xác nhận', 'Hủy', 'Hoàn thành' trong bảng danh sách lịch đặt.

## 3. Hoàn thiện & Kiểm thử

- [x] 3.1 Cấu hình `TempData` để hiển thị thông báo thành công/thất bại sau mỗi thao tác.
- [ ] 3.2 Kiểm tra tính toàn vẹn dữ liệu khi xóa khách hàng (có ảnh hưởng đến lịch đặt cũ không?).
- [ ] 3.3 Chạy thử toàn bộ luồng CRUD để đảm bảo không có lỗi 500.
