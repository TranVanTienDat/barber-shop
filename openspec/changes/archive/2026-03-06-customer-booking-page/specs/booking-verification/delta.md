## ADDED Requirements

### Requirement: Kiểm tra trùng lịch phía Server

Hệ thống phải kiểm tra lại tính khả dụng của slot giờ tại thời điểm gửi form.

#### Scenario: Đặt lịch thành công

- **WHEN** Khách hàng nhấn "Xác nhận" và slot giờ vẫn còn trống
- **THEN** Hệ thống lưu lịch hẹn, gửi mail thông báo và hiển thị trang thành công.

#### Scenario: Slot giờ vừa bị người khác chiếm

- **WHEN** Khách hàng nhấn "Xác nhận" nhưng slot giờ đã bị người khác đặt trước đó vài giây
- **THEN** Hệ thống từ chối lưu và yêu cầu khách hàng chọn khung giờ khác.
