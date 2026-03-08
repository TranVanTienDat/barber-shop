## 1. Backend Logic (AdminController)

- [x] 1.1 Thêm Action Method `Services` mẫu lọc và truy vấn dữ liệu từ DB thay vì trả về View trống.
- [x] 1.2 Thêm Action Method `CreateService` (GET/POST) để hiển thị form và lưu dịch vụ mới.
- [x] 1.3 Thêm Action Method `EditService` (GET/POST) để lấy thông tin dịch vụ và cập nhật thay đổi.
- [x] 1.4 Thêm Action Method `DeleteService` (POST) để xóa dịch vụ theo Id.
- [x] 1.5 Cấu hình `TempData` để truyền thông báo thành công/thất bại giữa các request.

## 2. UI Updates (Views)

- [x] 2.1 Cập nhật `Admin/Services.cshtml` để lặp qua danh sách `IEnumerable<BarberService>` thật.
- [x] 2.2 Tạo View `Admin/CreateService.cshtml` (hoặc tích hợp Modal) cho việc thêm mới.
- [x] 2.3 Tạo View `Admin/EditService.cshtml` (hoặc tích hợp Modal) cho việc chỉnh sửa.
- [x] 2.4 Tích hợp đoạn mã JavaScript/Modal xác nhận trước khi gọi Action Xóa.
- [x] 2.5 Hiển thị Banner thông báo dựa trên `TempData` trong Layout hoặc View cụ thể.

## 3. Validation & Refinement

- [x] 3.1 Thêm Data Annotations vào Model hoặc ViewModel để kiểm tra dữ liệu đầu vào.
- [x] 3.2 Kiểm tra logic hiển thị khi Database trống (Empty state).
- [x] 3.3 Đảm bảo UI Responsive và tuân thủ hoàn toàn Stitch UI System.
