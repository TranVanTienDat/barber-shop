## ADDED Requirements

### Requirement: Chọn ngày đặt lịch

Khách hàng chọn ngày muốn đến tiệm qua giao diện lịch.

#### Scenario: Chọn ngày hợp lệ

- **WHEN** Khách hàng chọn một ngày trong tương lai
- **THEN** Hệ thống hiển thị các khung giờ còn trống trong ngày đó.

### Requirement: Hiển thị khung giờ khả dụng

Hệ thống chỉ hiển thị các khung giờ chưa có ai đặt hoặc chưa đủ công suất.

#### Scenario: Giờ đã bị chiếm

- **WHEN** Một khung giờ đã có lịch xác nhận (`Confirmed`)
- **THEN** Khách hàng không thể chọn khung giờ đó (bị mờ hoặc ẩn).
