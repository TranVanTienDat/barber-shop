# HỆ THỐNG QUẢN LÝ ĐẶT LỊCH DỊCH VỤ Ở QUÁN CẮT TÓC

## 📋 Thông tin chung

- **Đơn vị phát triển:** Phi Long Software
- **Ngày bắt đầu:** 04/3/2026
- **Thời gian thực hiện:** 3 ngày
- **Mục tiêu:** Xây dựng hệ thống web đặt lịch dịch vụ cắt tóc cơ bản.

## 🛠 Yêu cầu kỹ thuật

- **Framework:** ASP.NET Core 7+
- **ORM:** Entity Framework Core (EF Core)
- **Database:** SQL Server hoặc SQLite (Ưu tiên SQLite để dễ triển khai ban đầu)
- **Frontend:** ASP.NET Core MVC hoặc Razor Pages

## 🚀 Chức năng chính

### 1. Tài khoản Quản trị viên (Admin)

- Đăng ký / Đăng nhập.
- Quản lý toàn bộ dịch vụ và đơn đặt lịch.

### 2. Dành cho Khách hàng (Booking)

- **Chọn dịch vụ:** Tên, Ảnh, Mô tả, Thời gian trung bình, Giá (Cho phép chọn nhiều).
- **Chọn ngày giờ:** Đặt lịch theo thời gian rảnh.
- **Thông tin khách hàng:** Tên, Số điện thoại, Email.
- **Ràng buộc:** Không đặt trùng giờ (Cùng lịch cắt vào cùng giờ).
- **Huỷ lịch:** Click vào đường link trong email thông báo để huỷ.

### 3. Quản lý (Admin Panel)

- **Quản lý dịch vụ:** Thêm, sửa, xoá, duyệt/huỷ duyệt (Chỉ hiện dịch vụ đã duyệt cho khách).
- **Quản lý lịch đặt:** Thêm, sửa, xoá, duyệt/huỷ duyệt lịch.
- **Xem danh sách:** Dạng lịch (Calendar View) và danh sách lọc theo khoảng thời gian.
- **Xuất dữ liệu:** Export Excel danh sách lịch đặt.
- **Paging & Filtering:** Phân trang và lọc trong danh sách.

### 4. Hệ thống Email & Thông báo

- Gửi mail xác nhận khi đặt lịch thành công.
- Gửi mail khi huỷ lịch thành công.

## 📡 API Endpoints

- `[GET] /api/services`: Danh sách dịch vụ cắt tóc.
- `[POST] /api/bookings`: Đặt lịch mới.
- `[GET] /api/bookings/user`: Lịch đã đặt của user.
- `[DELETE] /api/bookings/{id}`: Hủy lịch.
- `[GET] /api/admin/bookings`: Danh sách toàn bộ lịch (Dành cho Admin).

## 🖥 Giao diện (UI)

- **Giao diện Khách:** Trang đăng ký dịch vụ.
- **Giao diện Admin:**
  - Trang đăng nhập.
  - Trang quản lý loại dịch vụ.
  - Trang danh sách lịch đặt.

---

_Lưu ý: Bố cục rõ ràng, dễ hiểu, không yêu cầu CSS quá cầu kỳ._
