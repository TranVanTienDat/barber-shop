# Spec: booking-cancellation-ui

## Requirements

### Requirement: Cancellation Landing Page

Trang web phải hiển thị thông tin chi tiết về lịch hẹn để khách hàng xác nhận trước khi thực hiện hủy.

#### Scenario: Display booking details

- **WHEN** Khách hàng truy cập vào đường link hủy hợp lệ từ email.
- **THEN** Hệ thống hiển thị: Tên gói dịch vụ, Ngày, Giờ, và trạng thái hiện tại của lịch hẹn.
- **THEN** Hệ thống hiển thị nút "Xác nhận hủy lịch" nổi bật.

#### Scenario: Invalid or expired link

- **WHEN** Khách hàng truy cập vào đường link với Guid không tồn tại hoặc đã hết hạn/đã hủy.
- **THEN** Hệ thống hiển thị thông báo lỗi thân thiện và nút quay về trang chủ.

### Requirement: Cancellation Confirmation

Khách hàng phải xác nhận hành động hủy để tránh sơ suất.

#### Scenario: Confirm cancellation

- **WHEN** Khách hàng nhấn nút "Xác nhận hủy lịch".
- **THEN** Một yêu cầu POST được gửi tới hệ thống Backend.
- **THEN** Hiển thị trạng thái đang xử lý (loading state) trên nút bấm.

### Requirement: Success/Failure Feedback

Khách hàng cần biết kết quả của hành động hủy.

#### Scenario: Cancellation success

- **WHEN** Backend trả về kết quả thành công.
- **THEN** Hệ thống hiển thị Toast thông báo "Hủy lịch hẹn thành công" và cập nhật UI sang trạng thái đã hủy.

#### Scenario: Cancellation failure

- **WHEN** Backend trả về kết quả thất bại (ví dụ: lỗi kết nối).
- **THEN** Hệ thống hiển thị Toast thông báo lỗi "Không thể hủy lịch, vui lòng thử lại sau".
