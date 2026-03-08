## ADDED Requirements

### Requirement: BarberServices Table Schema

Hệ thống SHALL lưu trữ thông tin các dịch vụ cắt tóc với các trường dữ liệu định danh và mô tả chi tiết.

#### Scenario: Verify service structure

- **WHEN** xem cấu trúc bảng BarberServices
- **THEN** bảng phải bao gồm Id (INT PK), Name (NVARCHAR 200), Description (NVARCHAR MAX), ImageUrl (NVARCHAR 500), Duration (INT), Price (DECIMAL 18,2), và IsApproved (BIT).

### Requirement: Bookings Table Schema

Hệ thống SHALL lưu trữ thông tin đặt lịch của khách hàng với mã định danh duy nhất (GUID).

#### Scenario: Verify booking structure

- **WHEN** xem cấu trúc bảng Bookings
- **THEN** bảng phải bao gồm Id (UNIQUEIDENTIFIER PK), CustomerName (NVARCHAR 200), CustomerPhone (VARCHAR 20), CustomerEmail (VARCHAR 100), StartTime (DATETIME2), EndTime (DATETIME2), Status (INT/ENUM), và CancellationCode (VARCHAR 50).

### Requirement: BookingServices many-to-many relationship

Hệ thống SHALL hỗ trợ quan hệ nhiều-nhiều giữa Booking và BarberService thông qua bảng trung gian.

#### Scenario: Verify relationship link

- **WHEN** khách chọn 3 dịch vụ trong 1 lần đặt
- **THEN** hệ thống phải tạo 1 bản ghi trong Bookings và 3 bản ghi tương ứng trong BookingServices liên kết với các ServiceId khác nhau.

### Requirement: AdminAccounts Table Schema

Hệ thống SHALL lưu trữ tài khoản quản trị viên để bảo mật quyền truy cập.

#### Scenario: Verify admin structure

- **WHEN** xem cấu trúc bảng AdminAccounts
- **THEN** bảng phải bao gồm Id (INT PK), Username (NVARCHAR 100 UNIQUE), và PasswordHash (NVARCHAR MAX).
