## Why

Hiện tại, hệ thống BarberBookingWeb đang gửi email xác nhận ngay lập tức khi khách hàng đặt lịch và mặc định trạng thái là `Confirmed`. Ngoài ra, dịch vụ email hiện tại (`SimpleEmailService`) chỉ ghi log mà chưa thực hiện gửi email thật qua giao thức SMTP.

Thay đổi này nhằm mục đích:

- Thiết lập quy trình duyệt lịch hẹn: Khách đặt -> Chờ duyệt (`Pending`) -> Admin duyệt -> Gửi thông báo.
- Cung cấp khả năng gửi email thực tế để khách hàng nhận được thông tin lịch hẹn chính xác.

## What Changes

- **Trạng thái mặc định**: Thay đổi logic trong `BookingController` để mọi lịch hẹn mới đều ở trạng thái `Pending`.
- **Giao thức Email**: Triển khai `SmtpEmailService` để gửi mail thật.
- **Tự động hóa thông báo**: Tích hợp gọi dịch vụ email vào logic xử lý của Admin khi chuyển trạng thái lịch hẹn sang `Confirmed`.
- **Cấu hình**: Bổ sung các tham số cấu hình SMTP vào `appsettings.json`.

## Capabilities

### New Capabilities

- `email-notification`: Cung cấp dịch vụ gửi email thực tế thông qua cấu hình SMTP.
- `booking-approval-flow`: Quy trình xử lý nghiệp vụ cho phép Admin kiểm duyệt và gửi thông báo tự động khi chấp nhận lịch hẹn.

### Modified Capabilities

- (None)

## Impact

- **Controllers**: `BookingController.cs` (logic đặt lịch), `AdminController.cs` (logic duyệt lịch).
- **Services**: `IEmailService` (thực thi mới), `SimpleEmailService` (thay thế hoặc giữ lại làm fallback).
- **Configuration**: `appsettings.json`, `Program.cs`.
- **User Experience**: Khách hàng sẽ nhận được email trễ hơn (sau khi được duyệt) thay vì nhận ngay lập tức.
