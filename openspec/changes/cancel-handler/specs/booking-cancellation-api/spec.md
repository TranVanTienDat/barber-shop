# Spec: booking-cancellation-api

## Requirements

### Requirement: Cancellation Processing

Cung cấp endpoint để thực thi việc hủy lịch hẹn an toàn.

#### Scenario: Process valid cancellation

- **WHEN** Nhận yêu cầu POST với `CancellationGuid` hợp lệ.
- **THEN** Cập nhật thuộc tính `Status` của Booking tương ứng thành `Cancelled`.
- **THEN** Gửi email thông báo "Lịch hẹn đã bị hủy" đến email của khách hàng.
- **THEN** Trả về mã thành công (200 OK) kèm thông báo.

#### Scenario: Process invalid cancellation

- **WHEN** Nhận yêu cầu POST với `CancellationGuid` không tồn tại hoặc lịch đã ở trạng thái `Cancelled`.
- **THEN** Trả về mã lỗi (400 Bad Request) kèm thông báo lỗi chi tiết.
