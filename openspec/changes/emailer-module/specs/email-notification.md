## ADDED Requirements

### Requirement: Send Real Email via SMTP

Hệ thống PHẢI có khả năng gửi email thực tế thông qua giao thức SMTP phục vụ cho việc thông báo trạng thái lịch hẹn.

#### Scenario: Gửi email thành công

- **WHEN** Quản trị viên chấp nhận lịch hẹn
- **THEN** Hệ thống sử dụng thông tin SMTP đã cấu hình để gửi email tới địa chỉ của khách hàng.

### Requirement: Email Content Template

Hệ thống PHẢI có khả năng định dạng nội dung email với các thông tin động như tên khách hàng, thời gian, tên dịch vụ và thông tin cửa hàng.

#### Scenario: Định dạng email xác nhận

- **WHEN** Email xác nhận được tạo
- **THEN** Nội dung email PHẢI bao gồm: Tên khách hàng, Ngày giờ hẹn, Danh sách dịch vụ và Lời cảm ơn.
