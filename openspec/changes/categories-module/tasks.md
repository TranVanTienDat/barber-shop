## 1. Database & Models

- [ ] 1.1 Tạo model `Category` trong `Models/Category.cs` với các trường Id, Name, Description.
- [ ] 1.2 Thêm DbSet cho `Category` vào `Data/ApplicationDbContext.cs`.
- [ ] 1.3 Cập nhật model `BarberService`: thêm `CategoryId` và thuộc tính navigation `Category`.
- [ ] 1.4 Tạo và thực thi Migration để cập nhật Database, bao gồm logic chuyển đổi dữ liệu Category từ string sang table mới.

## 2. Admin Categories CRUD

- [ ] 2.1 Thêm các Action trong `AdminController`: `Categories`, `CreateCategory`, `EditCategory`, `DeleteCategory`.
- [ ] 2.2 Tạo View `Views/Admin/Categories.cshtml` để hiển thị danh sách danh mục.
- [ ] 2.3 Tạo View `Views/Admin/CreateCategory.cshtml` và `Views/Admin/EditCategory.cshtml`.
- [ ] 2.4 Thêm link "Quản lý danh mục" vào Sidebar của giao diện Admin.

## 3. Cập nhật Service Management

- [ ] 3.1 Cập nhật `AdminController.CreateService` và `EditService` (GET) để truyền danh sách `Categories` vào ViewBag/ViewModel.
- [ ] 3.2 Cập nhật View `Admin/CreateService.cshtml` và `EditService.cshtml`: Đổi ô nhập text Category thành `<select>` linh hoạt.
- [ ] 3.3 Cập nhật Action POST `CreateService` và `EditService` để lưu `CategoryId`.

## 4. Cập nhật Booking Interface

- [ ] 4.1 Cập nhật `BookingController.Index` để load danh sách `Categories` từ Database.
- [ ] 4.2 Cập nhật View `Views/Booking/Index.cshtml`: Hiển thị danh sách danh mục trong bộ lọc lấy từ bảng mới.
- [ ] 4.3 Kiểm tra khả năng lọc Client-side (JS) để đảm bảo vẫn hoạt động mượt mà với cấu trúc dữ liệu mới.

## 5. Kiểm thử & Hoàn thiện

- [ ] 5.1 Kiểm tra việc hiển thị đúng tên danh mục trong danh sách Services ở Admin.
- [ ] 5.2 Kiểm tra logic xóa danh mục: Không cho xóa nếu đang có dịch vụ liên kết (hoặc cảnh báo).
- [ ] 5.3 Verify toàn bộ luồng đặt lịch của khách hàng sau khi chuyển đổi.
