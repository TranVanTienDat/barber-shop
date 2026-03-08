## ADDED Requirements

### Requirement: Thu thập thông tin khách hàng

Khách hàng phải cung cấp thông tin liên hệ để hoàn tất đặt lịch.

#### Scenario: Nhập thông tin hợp lệ

- **WHEN** Khách hàng nhập Tên, SĐT hợp lệ và Email
- **THEN** Nút "Xác nhận đặt lịch" được kích hoạt.

#### Scenario: Thông tin thiếu hoặc sai định dạng

- **WHEN** Khách hàng để trống Tên/SĐT hoặc nhập sai định dạng Email
- **THEN** Hệ thống hiển thị cảnh báo lỗi và không cho phép gửi form.
