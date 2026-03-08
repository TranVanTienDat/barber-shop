## ADDED Requirements

### Requirement: Admin Login Form

Hệ thống SHALL cung cấp một giao diện đăng nhập dành cho quản trị viên với các trường nhập liệu: Username và Password.

#### Scenario: Display login form

- **WHEN** người dùng truy cập vào đường dẫn `/admin/login`
- **THEN** hệ thống hiển thị trang đăng nhập với logo và các ô nhập Username, Password.

### Requirement: Admin Authentication Logic

Hệ thống SHALL kiểm tra thông tin đăng nhập từ bảng `AdminAccounts`. Mật khẩu nhập vào phải khớp với mã hash được lưu trong cơ sở dữ liệu.

#### Scenario: Successful login

- **WHEN** người dùng nhập đúng Username và Password
- **THEN** hệ thống thiết lập Cookie xác thực và chuyển hướng người dùng đến trang Dashboard của Admin (`/admin/dashboard`).

#### Scenario: Failed login

- **WHEN** người dùng nhập sai Username hoặc Password
- **THEN** hệ thống hiển thị thông báo lỗi "Tên đăng nhập hoặc mật khẩu không chính xác" và vẫn ở lại trang đăng nhập.

### Requirement: Protected Admin Access

Tất cả các trang bắt đầu bằng `/admin` (ngoại trừ `/admin/login`) SHALL yêu cầu người dùng phải đăng nhập trước khi truy cập.

#### Scenario: Access without login

- **WHEN** người dùng chưa đăng nhập truy cập vào `/admin/dashboard`
- **THEN** hệ thống tự động chuyển hướng người dùng về trang `/admin/login`.
