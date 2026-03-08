## Why

Quản trị viên cần một giao diện chính thức để quản lý các gói dịch vụ của salon (thêm mới, chỉnh sửa thông tin, cập nhật giá và thời gian, xóa dịch vụ không còn cung cấp). Hiện tại giao diện quản trị mới chỉ dừng lại ở mức UI mẫu (Mockup), cần kết nối với Database để vận hành thật.

## What Changes

- **Backend**: Thêm các Action Methods (GET/POST) cho Create, Edit, Delete vào `AdminController`. Sử dụng `ApplicationDbContext` để thao tác dữ liệu.
- **Frontend**: Cập nhật trang `Admin/Services.cshtml` để hiển thị dữ liệu thật từ Database thay vì dữ liệu mẫu. Thêm các Form (Modals hoặc trang riêng) để nhập liệu.
- **Validation**: Thêm logic kiểm tra tính hợp lệ của dữ liệu dịch vụ (tên, giá, thời gian).

## Capabilities

### New Capabilities

- `service-management`: Cho phép Quản trị viên thực hiện toàn bộ vòng đời CRUD trên danh mục dịch vụ của salon.

### Modified Capabilities

- (None)

## Impact

- **Controllers**: `AdminController.cs` (Thêm logic xử lý).
- **Views**: `Admin/Services.cshtml` (Cập nhật binding dữ liệu và tương tác).
- **Models**: `BarberService.cs` (Đảm bảo các thuộc tính khớp với yêu cầu giao diện).
- **Database**: `barber.db` (Dữ liệu bảng `BarberServices`).
