# Tasks: Triển khai Emailer Module

## Bước 1: Hạ tầng (Infrastructure)

- [x] **T1.1**: Thêm `EmailSettings` vào `appsettings.json`.
- [x] **T1.2**: Tạo lớp `SmtpEmailService` kế thừa `IEmailService` sử dụng `System.Net.Mail`.
- [x] **T1.3**: Đăng ký `SmtpEmailService` trong `Program.cs`.

## Bước 2: Thay đổi logic đặt lịch (Booking)

- [x] **T2.1**: Truy cập `BookingController.Confirm` và đặt `Status = BookingStatus.Pending`.
- [x] **T2.2**: (Tùy chọn) Gửi một email thông báo "Đã nhận lịch và đang chờ duyệt".

## Bước 3: Thay đổi logic quản trị (Admin)

- [x] **T3.1**: Cấp quyền truy cập `IEmailService` (Constructor injection) cho `AdminController`.
- [x] **T3.2**: Triển khai logic gửi email trong `UpdateBookingStatus` khi chuyển trạng thái sang `Confirmed`.
- [x] **T3.3**: Định dạng nội dung email (Email Template) với đầy đủ thông tin khách đặt.

## Bước 4: Kiểm tra và Tối ưu (Verify)

- [x] **V4.1**: Test luồng đặt lịch mới (Kiểm tra trạng thái `Pending` trong DB).
- [x] **V4.2**: Test luồng duyệt lịch từ Admin (Kiểm tra xem email có được gửi đi không).
