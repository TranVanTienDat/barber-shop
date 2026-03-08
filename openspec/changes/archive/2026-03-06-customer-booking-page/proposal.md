## Why

Xây dựng giao diện đặt lịch chuyên nghiệp cho khách hàng, giúp họ dễ dàng tiếp cận dịch vụ, chọn thời gian phù hợp và đăng ký nhanh chóng. Điều này giúp giảm tải việc đặt lịch thủ công và chuyên nghiệp hóa quy trình phục vụ.

## What Changes

- Tạo trang đặt lịch công khai (`/Booking`) cho khách hàng.
- Tích hợp giao diện chọn dịch vụ trực quan (ảnh, mô tả, giá).
- Hệ thống chọn ngày giờ thông minh, tự động ẩn các khung giờ đã có người đặt.
- Quy trình đăng ký thông tin khách hàng đơn giản.
- Cơ chế hủy lịch an toàn qua link email không cần đăng nhập.

## Capabilities

### New Capabilities

- `service-selection-public`: Hiển thị danh sách dịch vụ đã duyệt, cho phép khách hàng chọn nhiều dịch vụ cùng lúc.
- `booking-scheduling-public`: Giao diện chọn ngày (calendar) và chọn giờ (time slots) còn trống.
- `customer-onboarding`: Form thu thập thông tin khách hàng nhạy bén.
- `booking-verification`: Logic kiểm tra trùng lịch phía Server và Client.
- `public-cancellation`: Khả năng hủy lịch thông qua ID bảo mật từ email.

### Modified Capabilities

- `booking-management`: Cập nhật logic quản lý để hỗ trợ các lịch đặt từ khách hàng công khai.

## Impact

- **Controllers**: Tạo `BookingController` mới cho phía khách hàng.
- **Views**: Thêm thư mục `Views/Booking` với các giao diện đặt lịch.
- **Logic**: Bổ sung hàm kiểm tra slot giờ trống trong `AdminController` hoặc một Service mới.
- **Database**: Không thay đổi schema nhưng sẽ có nhiều dữ liệu `Booking` hơn từ Public.
- **Email**: Cấu hình gửi mail thông báo kèm link hủy.
