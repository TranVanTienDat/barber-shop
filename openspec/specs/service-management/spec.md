## Requirements for Capability: service-management

### Requirement: Danh sách dịch vụ

Hệ thống phải hiển thị toàn bộ danh sách dịch vụ hiện có trong cơ sở dữ liệu lên giao diện Admin.

#### Scenario: Hiển thị dữ liệu thật

- **WHEN** Admin truy cập vào đường dẫn `/Admin/Services`
- **THEN** Hệ thống truy vấn bảng `BarberServices` và hiển thị danh sách dạng thẻ (Cards) với đầy đủ Tên, Giá, Thời gian và Hình ảnh.

### Requirement: Thêm mới dịch vụ

Admin có thể thêm các dịch vụ mới vào hệ thống.

#### Scenario: Lưu dịch vụ mới thành công

- **WHEN** Admin điền đầy đủ thông tin (Tên, Thể loại, Mô tả, Giá, Thời gian, URL ảnh) và nhấn "Lưu"
- **THEN** Hệ thống lưu bản ghi mới vào Database và hiển thị thông báo "Thêm dịch vụ thành công" tại trang danh sách.

### Requirement: Chỉnh sửa dịch vụ

Admin có thể cập nhật thông tin cho các dịch vụ đã tồn tại.

#### Scenario: Cập nhật thông tin dịch vụ

- **WHEN** Admin nhấn "Chỉnh sửa" trên một dịch vụ, thay đổi thông tin và nhấn "Cập nhật"
- **THEN** Hệ thống cập nhật bản ghi tương ứng trong Database và quay lại trang danh sách với thông báo thành công.

### Requirement: Xóa dịch vụ

Admin có thể loại bỏ các dịch vụ không còn cung cấp.

#### Scenario: Xác nhận và xóa

- **WHEN** Admin nhấn "Xóa" và xác nhận tại hộp thoại cảnh báo
- **THEN** Hệ thống xóa bản ghi khỏi Database và cập nhật lại giao diện danh sách.
