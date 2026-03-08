## MODIFIED Requirements

### Requirement: Thêm mới dịch vụ

Admin có thể thêm các dịch vụ mới vào hệ thống.

#### Scenario: Lưu dịch vụ mới thành công

- **WHEN** Admin điền đầy đủ thông tin (Tên, Thẻ loại chọn từ danh sách, Mô tả, Giá, Thời gian, URL ảnh) và nhấn "Lưu"
- **THEN** Hệ thống lưu bản ghi mới với CategoryId tương ứng vào Database và hiển thị thông báo "Thêm dịch vụ thành công" tại trang danh sách.

### Requirement: Chỉnh sửa dịch vụ

Admin có thể cập nhật thông tin cho các dịch vụ đã tồn tại.

#### Scenario: Cập nhật thông tin dịch vụ

- **WHEN** Admin nhấn "Chỉnh sửa" trên một dịch vụ, thay đổi thông tin (bao gồm chọn lại danh mục từ dropdown) và nhấn "Cập nhật"
- **THEN** Hệ thống cập nhật bản ghi tương ứng với CategoryId mới trong Database và quay lại trang danh sách với thông báo thành công.
