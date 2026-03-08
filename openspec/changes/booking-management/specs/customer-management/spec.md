## ADDED Requirements

### Requirement: Quản lý Danh sách Khách hàng

Hệ thống SHALL hiển thị toàn bộ danh sách khách hàng từ cơ sở dữ liệu lên giao diện Admin.

#### Scenario: Hiển thị thông tin khách hàng thực tế

- **WHEN** Admin truy cập trang `/Admin/Customers`
- **THEN** Hệ thống truy vấn bảng `AdminAccounts` và hiển thị Tên, SĐT, Email, Ngày đăng ký.

### Requirement: Thêm khách hàng mới

Admin SHALL có thể đăng ký thông tin cho khách hàng mới ngay trong trang quản trị.

#### Scenario: Lưu thông tin khách hàng thành công

- **WHEN** Admin điền Tên, SĐT, Email và nhấn "Lưu"
- **THEN** Bản ghi được khởi tạo trong Database và Admin nhận được thông báo thành công.

### Requirement: Xóa khách hàng

Admin SHALL có thể loại bỏ thông tin khách hàng khỏi hệ thống.

#### Scenario: Xóa và cập nhật danh sách

- **WHEN** Admin nhấn "Xóa" một khách hàng và xác nhận
- **THEN** Hệ thống xóa khách hàng đó và các liên kết liên quan (nếu có) và tải lại danh sách.
