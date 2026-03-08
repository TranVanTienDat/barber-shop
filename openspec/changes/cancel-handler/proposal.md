## Why

Hiện tại, khi khách hàng đặt lịch hẹn, hệ thống sẽ gửi một email thông báo. Tuy nhiên, khách hàng không có phương thức thuận tiện để tự hủy lịch hẹn nếu họ thay đổi kế hoạch. Điều này buộc họ phải liên hệ trực tiếp với Admin, gây mất thời gian cho cả hai bên. Mục tiêu của thay đổi này là cung cấp một quy trình tự hủy (self-service cancellation) đơn giản và trực quan cho khách hàng qua email.

## What Changes

1. **Email Template**: Cập nhật logic gửi email xác nhận đặt lịch để đính kèm một đường link duy nhất dẫn đến trang hủy lịch.
2. **Cancellation UI**: Tạo một trang giao diện người dùng (FE) đơn giản dành cho khách hàng. Trang này sẽ:
   - Hiển thị thông tin lịch hẹn: Tên gói (Service Name), Ngày (Date), Giờ (Time).
   - Có nút xác nhận "Hủy lịch".
3. **Backend Logic**: Phát triển API xử lý yêu cầu hủy. Khi người dùng xác nhận trên UI, FE sẽ gửi yêu cầu về BE để cập nhật trạng thái lịch hẹn trong cơ sở dữ liệu.
4. **User Feedback**: Tích hợp thông báo Toast trên giao diện để báo cáo kết quả hủy (Thành công/Thất bại).

## Capabilities

### New Capabilities

- `booking-cancellation-ui`: Giao diện khách hàng dùng để xem thông tin và xác nhận hủy lịch hẹn.
- `booking-cancellation-api`: Cung cấp endpoint để xử lý việc thay đổi trạng thái booking thành "Cancelled" và thực hiện các logic bổ trợ (nếu có).

### Modified Capabilities

- `email-notification`: Cập nhật nội dung email gửi cho khách hàng để bao gồm liên kết hủy lịch.

## Impact

- **Backend**: Cần mở rộng `BookingController` hoặc tạo mới một Controller cho khách hàng để xử lý logic hủy.
- **Frontend**: Thêm một View mới cho trang hủy lịch, sử dụng lại các component UI hiện có (DaisyUI).
- **Email Service**: Cần truyền thêm thông tin (như ID hoặc cancellation token) vào template email.
- **Database**: Cập nhật giá trị enum/trạng thái cho cột `Status` của bảng Booking (nếu chưa có trạng thái 'Cancelled').
