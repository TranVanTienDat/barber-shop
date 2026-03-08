## ADDED Requirements

### Requirement: Quản lý Lịch đặt (Bookings)

Admin SHALL có quyền xem và thao tác trên các yêu cầu đặt lịch của khách hàng.

#### Scenario: Hiển thị danh sách lịch hẹn

- **WHEN** Admin truy cập trang `/Admin/Bookings`
- **THEN** Hệ thống hiển thị bảng gồm: Tên khách, Dịch vụ, Ngày giờ, Giá tiền và Trạng thái.

### Requirement: Cập nhật trạng thái lịch hẹn

Admin SHALL có thể thay đổi tình trạng của một đơn đặt lịch.

#### Scenario: Thay đổi sang "Đã hoàn thành" hoặc "Đã hủy"

- **WHEN** Admin chọn trạng thái mới cho lịch hẹn và nhấn "Cập nhật"
- **THEN** Cơ sở dữ liệu lưu trạng thái mới và hiển thị màu sắc tương ứng trên UI.

### Requirement: Xóa lịch hẹn

Admin SHALL có thể loại bỏ các lịch hẹn không hợp lệ hoặc do khách hàng yêu cầu xóa trắng.

#### Scenario: Xóa lịch hẹn vĩnh viễn

- **WHEN** Admin nhấn "Xóa" tại một dòng lịch hẹn
- **THEN** Hệ thống thực hiện Hard-delete (hoặc Soft-delete tùy cấu hình) và thông báo thành công.
