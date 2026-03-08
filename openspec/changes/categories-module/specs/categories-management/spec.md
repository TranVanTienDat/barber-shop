## ADDED Requirements

### Requirement: Danh sách danh mục

Hệ thống SHALL hiển thị danh sách toàn bộ các danh mục dịch vụ trong trang quản trị Admin.

#### Scenario: Xem danh sách danh mục

- **WHEN** Admin truy cập vào đường dẫn `/Admin/Categories`
- **THEN** Hệ thống hiển thị bảng chứa danh sách các danh mục gồm Tên và Mô tả.

### Requirement: Thêm mới danh mục

Admin MUST có khả năng tạo thêm các danh mục dịch vụ mới để phân loại.

#### Scenario: Tạo danh mục thành công

- **WHEN** Admin nhập Tên danh mục (bắt buộc) và Mô tả, sau đó nhấn "Lưu"
- **THEN** Hệ thống lưu danh mục vào Database và hiển thị thông báo "Thêm danh mục thành công".

### Requirement: Chỉnh sửa danh mục

Admin MUST có khả năng cập nhật lại thông tin (Tên, Mô tả) của các danh mục đã tồn tại.

#### Scenario: Cập nhật danh mục

- **WHEN** Admin thay đổi thông tin danh mục và nhấn "Cập nhật"
- **THEN** Hệ thống cập nhật bản ghi trong Database và hiển thị thông báo thành công.

### Requirement: Xóa danh mục

Admin MUST có khả năng xóa các danh mục không còn sử dụng.

#### Scenario: Xóa danh mục trống

- **WHEN** Admin nhấn "Xóa" trên một danh mục không có dịch vụ nào liên kết
- **THEN** Hệ thống xóa danh mục và cập nhật lại danh sách.
