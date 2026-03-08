# 🗄️ THIẾT KẾ CƠ SỞ DỮ LIỆU

## 1. Bảng `BarberServices` (Các dịch vụ cắt tóc)

- `Id`: Int (Primary Key)
- `Name`: String (Tên dịch vụ)
- `Description`: String (Mô tả)
- `ImageUrl`: String (Đường dẫn ảnh)
- `Duration`: Int (Thời gian trung bình - phút)
- `Price`: Decimal (Giá tiền)
- `IsApproved`: Bool (Trạng thái duyệt của Admin)

## 2. Bảng `Bookings` (Lịch đặt)

- `Id`: Guid (Primary Key)
- `CustomerName`: String
- `CustomerPhone`: String
- `CustomerEmail`: String
- `StartTime`: DateTime (Giờ bắt đầu)
- `EndTime`: DateTime (Giờ kết thúc - tự động cộng từ Services)
- `Status`: Enum (Pending, Confirmed, Cancelled)
- `CancellationCode`: String (Mã bí mật để khách tự hủy qua link)

## 3. Bảng `BookingServices` (Quan hệ Nhiều-Nhiều)

- `BookingId`: Guid
- `ServiceId`: Int
- (Xử lý việc khách chọn nhiều dịch vụ cùng lúc)

## 4. Bảng `AdminAccounts`

- `Id`: Int
- `Username`: String
- `PasswordHash`: String
