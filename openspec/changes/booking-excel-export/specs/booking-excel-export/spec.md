# Capability Spec: booking-excel-export

## ADDED Requirements

### Requirement: Xuất dữ liệu lịch đặt ra Excel

Hệ thống PHẢI cho phép Quản trị viên tải xuống tệp danh sách các lịch đặt hẹn dưới định dạng Excel (.xlsx).

#### Scenario: Xuất thành công với bộ lọc hiện tại

- **WHEN** Quản trị viên nhấn nút "Xuất Excel" trên trang Quản lý lịch đặt.
- **THEN** Hệ thống PHẢI tạo tệp Excel dựa trên các tiêu chí lọc đang áp dụng (Từ khóa, Ngày bắt đầu, Ngày kết thúc, Trạng thái).
- **THEN** Tệp Excel PHẢI chứa các cột: STT, Tên khách hàng, Số điện thoại, Email, Dịch vụ, Ngày, Giờ bắt đầu, Giờ kết thúc, Trạng thái.
- **THEN** Tình trạng phân trang (Pagination) KHÔNG ĐƯỢC làm hạn chế số lượng bản ghi trong file xuất (phải xuất toàn bộ kết quả lọc).

#### Scenario: Không có dữ liệu để xuất

- **WHEN** Bộ lọc không trả về bản ghi nào và người dùng nhấn "Xuất Excel".
- **THEN** Hệ thống PHẢI trả về một file Excel chỉ có dòng tiêu đề (Header) hoặc thông báo không có dữ liệu.

### Requirement: Định dạng file Excel chuyên nghiệp

Tệp Excel PHẢI được định dạng để người dùng có thể sử dụng ngay mà không cần chỉnh sửa nhiều.

#### Scenario: Định dạng cột và font chữ

- **WHEN** File Excel được mở.
- **THEN** Các cột PHẢI được tự động căn chỉnh độ rộng (Auto-fit).
- **THEN** Dòng tiêu đề PHẢI được in đậm để dễ phân biệt.
- **THEN** Các cột ngày tháng và giờ PHẢI hiển thị theo định dạng Việt Nam (`dd/MM/yyyy` và `HH:mm`).
