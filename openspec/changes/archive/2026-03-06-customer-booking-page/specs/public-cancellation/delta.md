## ADDED Requirements

### Requirement: Hủy lịch qua Link bảo mật

Khách hàng có thể hủy lịch mà không cần đăng nhập qua link trong Email.

#### Scenario: Hủy lịch thành công

- **WHEN** Khách hàng nhấn vào link chứa mã Token hợp lệ
- **THEN** Hệ thống chuyển trạng thái lịch sang "Cancelled", gửi mail xác nhận hủy và thông báo thành công cho khách.

#### Scenario: Token hết hạn hoặc sai

- **WHEN** Khách hàng sử dụng link với Token không tồn tại
- **THEN** Hệ thống hiển thị thông báo "Link không hợp lệ hoặc đã hết hạn".
