## MODIFIED Requirements

### Requirement: Admin Account Password Storage

Dữ liệu mật khẩu quản trị viên SHALL được lưu dưới dạng mã hóa (Hash) mạnh (ví dụ: BCrypt) thay vì văn bản thuần túy hoặc mã hóa yếu.

#### Scenario: Verify password hash

- **WHEN** một bản ghi mới được thêm vào bảng `AdminAccounts`
- **THEN** trường `PasswordHash` phải chứa chuỗi ký tự đã được băm.
