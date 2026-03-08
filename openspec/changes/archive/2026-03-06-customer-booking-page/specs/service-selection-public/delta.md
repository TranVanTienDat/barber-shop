## ADDED Requirements

### Requirement: Hiển thị danh sách dịch vụ công khai

Hệ thống phải hiển thị các dịch vụ đã được duyệt (`IsApproved = true`) cho khách hàng chọn.

#### Scenario: Xem danh sách dịch vụ

- **WHEN** Khách hàng truy cập trang đặt lịch
- **THEN** Hệ thống hiển thị các thẻ dịch vụ kèm Tên, Ảnh, Mô tả, Giá và Thời gian trung bình.

### Requirement: Chọn nhiều dịch vụ

Khách hàng có thể chọn một hoặc nhiều dịch vụ trong cùng một lần đặt lịch.

#### Scenario: Chọn và tính tổng tiền

- **WHEN** Khách hàng nhấn chọn nhiều dịch vụ
- **THEN** Hệ thống cập nhật danh sách đã chọn và hiển thị tổng số tiền dự kiến.
