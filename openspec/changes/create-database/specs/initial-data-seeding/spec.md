## ADDED Requirements

### Requirement: Default Barber Services Seeding

Hệ thống SHALL tự động tạo dữ liệu cho các dịch vụ cắt tóc cơ bản nếu bảng BarberServices đang trống.

#### Scenario: Seed basic services

- **WHEN** ứng dụng chạy lần đầu sau khi tạo DB
- **THEN** bảng BarberServices phải có ít nhất các dịch vụ: "Cắt tóc nam", "Cắt tóc & Gội đầu", "Combo VIP 7 bước", "Cạo mặt truyền thống".

### Requirement: Service Details Consistency

Dữ liệu mẫu cho dịch vụ SHALL phản ánh đúng các thông tin từ thiết kế UI trên Stitch.

#### Scenario: Verify service price and duration

- **WHEN** kiểm tra dịch vụ "Cắt tóc nam" trong DB
- **THEN** trường Price và Duration phải khớp với giá niêm yết dự kiến (ví dụ: 80,000đ - 30 phút).

### Requirement: Default Admin Account Seeding

Hệ thống SHALL tạo một tài khoản quản trị mặc định để có thể đăng nhập ngay sau khi cài đặt.

#### Scenario: Seed admin account

- **WHEN** database được khởi tạo lần đầu
- **THEN** bảng AdminAccounts phải chứa tài khoản với username "admin" và một PasswordHash hợp lệ (không lưu plain text).
