## Context

Hiện tại, hệ thống lưu trữ danh mục dịch vụ dưới dạng chuỗi văn bản (`string`) trực tiếp trong bảng `BarberServices`. Điều này dẫn đến dữ liệu không đồng nhất, khó quản lý và giới hạn khả năng mở rộng (như thêm mô tả hoặc hình ảnh cho danh mục). Hệ thống đang chạy trên nền tảng ASP.NET Core với Entity Framework Core.

## Goals / Non-Goals

**Goals:**

- Chuẩn hóa cấu trúc dữ liệu bằng cách tách `Category` thành một thực thể riêng biệt (mối quan hệ 1-N).
- Xây dựng giao diện quản lý danh mục hoàn chỉnh trong phần Admin.
- Cập nhật logic lọc dịch vụ ở trang đặt lịch để sử dụng bảng danh mục mới.
- Đảm bảo chuyển đổi dữ liệu an toàn từ cấu trúc cũ sang mới.

**Non-Goals:**

- Không triển khai hệ thống danh mục đa cấp (parent-child categories) trong phiên bản này.
- Không thay đổi logic cốt lõi của việc tính toán thời gian và giá dịch vụ.

## Decisions

1. **Cấu trúc Model**:
   - Tạo model `Category` với các trường: `Id` (int, PK), `Name` (string, Required), `Description` (string, Optional).
   - Cập nhật model `BarberService`: Thay thế trường `Category` (string) bằng `CategoryId` (int, FK) và navigation property `Category`.
   - _Lý do_: Đáp ứng chuẩn thiết kế cơ sở dữ liệu quan hệ, giúp tối ưu truy vấn và nhất quán dữ liệu.

2. **Chiến lược Di chuyển Dữ liệu (Migration)**:
   - Sử dụng EF Core Migration.
   - Viết logic trong file Migration:
     - 1. Tạo bảng `Categories`.
     - 2. Quét bảng `BarberServices` hiện tại, lấy danh sách `Category` (string) duy nhất và chèn vào bảng `Categories`.
     - 3. Ánh xạ ngược lại `CategoryId` mới cho các bản ghi `BarberServices`.
     - 4. Xóa cột `Category` cũ sau khi di chuyển thành công.
   - _Lý do_: Đảm bảo không làm gãy (breaking change) dữ liệu của Lão đại khi cập nhật code mới.

3. **Cập nhật Controllers & Views**:
   - `AdminController`: Thêm các action CRUD cho `Category`. Cập nhật `GetServices` để join với bảng `Category` nhằm hiển thị tên danh mục.
   - `BookingController`: Cập nhật action `Index` để lấy danh sách `Categories` từ Database thay vì dùng `.Select(s => s.Category).Distinct()`.
   - _Lý do_: Tối ưu hóa hiệu suất truy vấn ở backend.

4. **Trải nghiệm Người dùng (UX)**:
   - Ở trang Admin, form thêm dịch vụ sẽ dùng thẻ `<select>` thay vì `<input type="text">` cho phần danh mục.
   - Ở trang Booking, bộ lọc danh mục sẽ hiển thị dựa trên dữ liệu thật từ bảng `Categories`.
   - _Lý do_: Giảm thiểu sai sót nhập liệu và tăng tính chuyên nghiệp.

## Risks / Trade-offs

- **[Rủi rủi]** Lỗi trong quá trình Migration dẫn đến mất liên kết dịch vụ → **[Khắc phục]** Kiểm tra kỹ script migration trên database kiểm thử trước khi áp dụng.
- **[Đánh đổi]** Tăng độ phức tạp của code do phải quản lý thêm một thực thể và các mối quan hệ liên quan → **[Lý do]** Sự đánh đổi này là cần thiết để hệ thống bền vững và dễ mở rộng về sau.
- **[Rủi ro]** Ảnh hưởng đến giao diện hiện tại của Lão đại → **[Khắc phục]** Giữ nguyên các class CSS của DaisyUI và chỉ thay đổi cấu trúc dữ liệu truyền vào.
