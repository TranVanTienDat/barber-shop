## ADDED Requirements

### Requirement: Cancellation Link in Email

Email thông báo (bao gồm cả email "Đang chờ duyệt" và "Đã xác nhận") PHẢI đính kèm đường link dẫn đến trang hủy lịch hẹn.

#### Scenario: Include Cancellation Link

- **WHEN** Email thông báo được tạo và gửi đi.
- **THEN** Nội dung email PHẢI bao gồm một đường link duy nhất được tạo dựa trên `CancellationGuid` của Booking đó.
- **THEN** Link có định dạng rõ ràng và kèm theo hướng dẫn để khách hàng có thể tự hủy nếu cần.
