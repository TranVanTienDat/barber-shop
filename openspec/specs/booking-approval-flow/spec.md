## ADDED Requirements

### Requirement: Default Pending Status

Mọi lịch hẹn mới được tạo từ giao diện khách hàng PHẢI được thiết lập ở trạng thái `Pending` (Chờ duyệt).

#### Scenario: Khách hàng đặt lịch thành công

- **WHEN** Khách hàng nhấn nút đặt lịch
- **THEN** Một bản ghi mới được tạo trong cơ sở dữ liệu với trạng thái là `Pending`.

### Requirement: Approval Notification Trigger

Hệ thống PHẢI tự động gửi email thông báo cho khách hàng ngay KHI Quản trị viên chuyển trạng thái lịch hẹn từ `Pending` sang `Confirmed`.

#### Scenario: Quản trị viên duyệt lịch hẹn

- **WHEN** Quản trị viên nhấn nút duyệt lịch hẹn trên bảng điều khiển
- **THEN** Trạng thái lịch cập nhật thành `Confirmed` VÀ hệ thống thực hiện gửi email thông báo thành công cho khách hàng.
