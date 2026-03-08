# 📦 Danh Sách Tính Năng - Barber Booking System

> Tài liệu liệt kê toàn bộ các tính năng đã được implement trong hệ thống.

---

## 1. 🌐 Giao Diện Khách Hàng (Booking)

### 1.1 Xem Dịch Vụ

- Hiển thị danh sách tất cả dịch vụ đã được Admin duyệt (`IsApproved = true`)
- Hiển thị ảnh, tên, mô tả, thời gian thực hiện, giá của từng dịch vụ
- Lọc dịch vụ theo **Danh mục (Category)**

### 1.2 Đặt Lịch

- Chọn **nhiều dịch vụ** cùng lúc
- Chọn **ngày** từ lịch (date picker)
- Chọn **khung giờ** (8:30 - 20:30, mỗi slot 30 phút)
- Hệ thống tự động **kiểm tra trùng lịch** theo thời gian thực (API)
- Trạng thái ban đầu khi đặt: **Chờ duyệt (Pending)**
- Nhập thông tin cá nhân: Họ tên, Số điện thoại, Email, Ghi chú

### 1.3 Email Tự Động

- Gửi email **xác nhận tiếp nhận** ngay sau khi đặt lịch thành công (trạng thái Pending)
- Email kèm **link hủy lịch** (unique GUID per booking)

### 1.4 Hủy Lịch

- Khách hàng click link hủy trong email
- Hệ thống hiển thị trang xác nhận hủy
- Sau khi hủy, gửi email **thông báo hủy thành công**

---

## 2. 🔐 Hệ Thống Xác Thực (Admin)

- Đăng nhập bằng Username / Password (BCrypt hashing)
- Cookie-based Authentication (lưu session 7 ngày)
- "Remember Me" option
- Đăng xuất
- Phân quyền: chỉ tài khoản `Role = Admin` mới truy cập được Admin Panel

---

## 3. 🛠️ Admin Panel

### 3.1 Quản Lý Lịch Đặt (`/Admin/Bookings`)

- Xem danh sách toàn bộ lịch đặt
- **Tìm kiếm** theo tên, số điện thoại, email khách hàng (case-insensitive)
- **Lọc** theo trạng thái (Pending / Confirmed / Completed / Cancelled) và theo khoảng ngày
- **Phân trang** (mặc định 10 items/trang)
- **Cảnh báo chồng lịch**: highlight các booking bị trùng giờ
- Tạo lịch hẹn mới (admin tạo thay)
- Cập nhật trạng thái lịch: Pending → Confirmed / Completed / Cancelled
- Xóa lịch hẹn
- **Email tự động** khi admin duyệt (Confirmed) hoặc hủy (Cancelled)
- **Xuất Excel** danh sách lịch hẹn (có hỗ trợ filter trước khi export)

### 3.2 Quản Lý Dịch Vụ (`/Admin/Services`)

- Xem danh sách tất cả dịch vụ
- Tìm kiếm theo tên, danh mục (case-insensitive)
- Lọc theo Danh mục, khoảng giá
- Phân trang
- Thêm dịch vụ: Tên, Mô tả, Giá, Thời gian, Danh mục, **Upload ảnh**
- Sửa dịch vụ (bao gồm thay ảnh)
- Xóa dịch vụ
- Duyệt / Chưa duyệt dịch vụ (chỉ dịch vụ được duyệt mới hiển thị cho khách)

### 3.3 Quản Lý Danh Mục (`/Admin/Categories`)

- Xem danh sách danh mục
- Tìm kiếm theo tên, mô tả
- Phân trang
- Thêm danh mục: Tên, Mô tả
- Sửa danh mục
- Xóa danh mục (không cho xóa nếu còn dịch vụ đang liên kết)

### 3.4 Quản Lý Khách Hàng (`/Admin/Customers`)

- Xem danh sách khách hàng
- Tìm kiếm theo tên, số điện thoại, email
- Phân trang
- Thêm khách hàng mới (mật khẩu mặc định `123456`)
- Sửa thông tin khách hàng
- Xóa khách hàng

### 3.5 Cấu Hình Email (`/Admin/EmailSettings`)

- Cấu hình SMTP: Server, Port, Sender Name, Sender Email, Username, Password, SSL
- Lưu cấu hình vào database (bảng `AppSettings`)
- Áp dụng ngay mà không cần restart ứng dụng

---

## 4. 📡 API Endpoints

| Method | Endpoint                                        | Mô tả                                                                |
| ------ | ----------------------------------------------- | -------------------------------------------------------------------- |
| `GET`  | `/api/bookings/available-slots?date=YYYY-MM-DD` | Trả về danh sách slot giờ (8:30–20:30) với trạng thái available/busy |

---

## 5. ⚙️ Tính Năng Hệ Thống

- **Seed data tự động**: Admin mặc định, danh mục, dịch vụ mẫu, khách hàng mẫu, lịch hẹn mẫu được tạo khi khởi động lần đầu
- **Upload ảnh dịch vụ**: Lưu tại `wwwroot/images/services/`
- **Validation chồng lịch**: Kiểm tra cả phía client (UI) và server
- **Email HTML**: Template email chuyên nghiệp với nút hủy lịch
