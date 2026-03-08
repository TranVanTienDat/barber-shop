## ADDED Requirements

### Requirement: Lọc dịch vụ theo danh mục chuẩn hóa

Giao diện đặt lịch PHẢI hiển thị danh sách các danh mục được lấy trực tiếp từ bảng dữ liệu `Categories`.

#### Scenario: Hiển thị bộ lọc danh mục

- **WHEN** Khách hàng truy cập trang đặt lịch `/Booking/Index`
- **THEN** Dropdown "Danh mục" hiển thị đầy đủ các tên danh mục có trong Database (thay vì dùng Distinct từ bảng dịch vụ).
- **THEN** Việc chọn một danh mục sẽ lọc và hiển thị chính xác các dịch vụ thuộc CategoryId đó.
