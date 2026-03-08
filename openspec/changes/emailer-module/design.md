## Design: Email & Approval Module

### Cấu trúc lớp (Class Architecture)

1. **Email Service Implementation**:
   - **`SmtpEmailService`**: Triển khai `IEmailService`, sử dụng `System.Net.Mail` (hoặc `MailKit` nếu cần) để gửi email.
   - **Cấu hình SMTP**: Các tham số (`Host`, `Port`, `Username`, `Password`, `EnableSsl`) lưu trong `appsettings.json`.

2. **Controller Logic Updates**:
   - **`BookingController`**: Sửa đổi luồng `Confirm` để đặt `Status = BookingStatus.Pending`.
   - **`AdminController`**:
     - Thêm dependency `IEmailService`.
     - Cập nhật hàm `UpdateBookingStatus`: Nếu trạng thái mới là `Confirmed`, thực hiện gọi `SendEmailAsync`.

### Cấu trúc dữ liệu cấu hình (App Settings)

```json
"EmailSettings": {
  "SmtpServer": "smtp.gmail.com",
  "Port": 587,
  "SenderName": "Barber Shop",
  "SenderEmail": "your-email@gmail.com",
  "Username": "your-email@gmail.com",
  "Password": "your-app-password",
  "EnableSsl": true
}
```

### Quy trình nghiệp vụ (Workflow)

1. **Khách hàng**: Đặt lịch -> Lưu DB (`Pending`).
2. **Hệ thống**: Không gửi email lúc này hoặc gửi email "Đang chờ duyệt".
3. **Admin**: Vào danh sách Bookings -> Nhấn "Approve" (Chuyển sang `Confirmed`).
4. **Hệ thống**: Cập nhật DB -> Gọi `EmailService.SendEmailAsync` tới khách hàng.

### Giao diện thông báo

Sử dụng HTML Email Template đơn giản nhưng chuyên nghiệp (Logo, Bảng dịch vụ, Lời nhắn).
