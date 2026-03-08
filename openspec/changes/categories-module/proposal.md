## Why

Hiện tại, thuộc tính `Category` trong model `BarberService` đang được lưu dưới dạng chuỗi văn bản tự do (string). Điều này gây ra một số vấn đề:

- Dễ sai sót chính tả khi nhập liệu, dẫn đến dữ liệu không nhất quán.
- Khó quản lý tập trung và mở rộng thêm các thuộc tính cho danh mục (ví dụ: mô tả danh mục, thứ tự hiển thị).
- Hiệu suất lọc dữ liệu trên quy mô lớn không cao.
- Giao diện đặt lịch cho khách hàng đang phải dùng hàm `Distinct()` trên bảng dịch vụ để lấy danh sách danh mục, gây lãng phí tài nguyên.

Việc chuyển đổi sang module `Categories` riêng biệt với quan hệ 1-N sẽ chuẩn hóa bộ dữ liệu và cải thiện trải nghiệm quản trị lẫn người dùng.

## What Changes

- **Database & Model**:
  - Thêm thực thể `Category` mới (Id, Name, Description).
  - Cập nhật `BarberService` để sử dụng khóa ngoại `CategoryId` thay cho trường `Category` (string).
  - Thực hiện Data Migration để chuyển đổi dữ liệu hiện có từ chuỗi sang liên kết với bảng mới.
- **Admin Interface**:
  - Xây dựng module CRUD hoàn chỉnh cho Danh mục (danh sách, thêm, sửa, xóa).
  - Cập nhật form Thêm/Sửa dịch vụ: Thay đổi ô nhập liệu Category (text) thành một dropdown chọn từ danh sách Category có sẵn.
- **Booking Interface**:
  - Cập nhật trang đặt lịch cho khách hàng: Load danh sách bộ lọc danh mục trực tiếp từ bảng `Categories`.
  - Giữ nguyên trải nghiệm lọc Client-side nhưng dựa trên dữ liệu chuẩn hóa.

## Capabilities

### New Capabilities

- `categories-management`: Cung cấp khả năng quản lý danh mục dịch vụ tập trung trong hệ thống Admin.

### Modified Capabilities

- `service-management`: Thay đổi cách thức lưu trữ và chỉ định thể loại cho dịch vụ (từ string sang CategoryId).
- `booking-approval-flow`: Cập nhật logic hiển thị và lọc dịch vụ theo danh mục mới trên trang đặt lịch của khách hàng.

## Impact

- **Database**: Bảng `BarberServices` và bảng `Categories` mới.
- **Controllers**: `AdminController` (thêm action cho category, sửa service), `BookingController` (sửa index load categories).
- **Views**:
  - Thêm `Admin/Categories.cshtml`, `Admin/CreateCategory.cshtml`, `Admin/EditCategory.cshtml`.
  - Sửa `Admin/Services.cshtml`, `Admin/CreateService.cshtml`, `Views/Booking/Index.cshtml`.
- **Logic**: Migration script/logic để không làm mất dữ liệu Category hiện có của các dịch vụ.
