## Context

Hệ thống hiện tại đã có bộ khung Admin quản lý dịch vụ và lịch hẹn. Chúng ta cần mở rộng một giao diện công khai cho khách hàng để họ có thể tự đặt lịch mà không cần tài khoản.

## Goals / Non-Goals

**Goals:**

- Triển khai trang `/Booking` với trải nghiệm người dùng hiện đại (Stitch style).
- Cho phép chọn nhiều dịch vụ cùng lúc.
- Kiểm tra tính khả dụng của thời gian thực (Real-time availability).
- Quy trình đăng ký thông tin khách hàng tinh gọn.
- Link hủy lịch bảo mật gửi qua Email.

**Non-Goals:**

- Chưa triển khai thanh toán online (chỉ đặt chỗ).
- Không yêu cầu khách hàng đăng nhập/đăng ký tài khoản (Guest booking).

## Decisions

### 1. Kiến trúc Giao diện (Frontend)

- **Hướng tiếp cận**: Sử dụng Single Page Booking Flow (JavaScript + Fetch API) để quá trình chọn dịch vụ và giờ diễn ra mượt mà không load lại trang.
- **Lý do**: Tăng trải nghiệm người dùng (UX) giống như giao dịch ứng dụng di động.

### 2. Xử lý Trùng lịch (Concurrency & Validation)

- **Hướng tiếp cận**:
  - Client-side: Vô hiệu hóa các khung giờ đã có lịch xác nhận.
  - Server-side: Kiểm tra lại một lần nữa trước khi lưu bản ghi vào Database.
- **Lý do**: Đảm bảo tính toàn vẹn dữ liệu ngay cả khi có nhiều người đặt cùng lúc.

### 3. Cơ chế Hủy lịch (Public Cancellation)

- **Hướng tiếp cận**: Sử dụng `Guid` làm mã định danh duy nhất cho mỗi `Booking`. Link hủy sẽ có dạng `/Booking/Cancel/{Guid}`.
- **Lý do**: Không yêu cầu khách hàng đăng nhập nhưng vẫn đảm bảo bảo mật (chỉ người nhận email mới biết link này).

### 4. Gửi Email thông báo

- **Hướng tiếp cận**: Tạo một `EmailService` giả lập (Interface IEmailService) ghi log ra Console hoặc File để kiểm tra trong giai đoạn phát triển, sẵn sàng cấu hình SMTP thực tế.

## Risks / Trade-offs

- [Rủi ro] Nhiều khách hàng chọn cùng một khung giờ cùng lúc → [Giảm thiểu] Validate chặt chẽ ở bước nhấn "Xác nhận" cuối cùng.
- [Rủi ro] Link hủy bị lộ → [Giảm thiểu] Link chỉ có tác dụng một lần và có mã Guid dài khó đoán.
