# 📖 Hướng Dẫn Sử Dụng - Barber Booking System

> Tài liệu hướng dẫn sử dụng hệ thống dành cho Khách hàng và Quản trị viên.

---

## PHẦN 1: KHÁCH HÀNG

### Bước 1 — Truy cập trang đặt lịch

Mở trình duyệt và truy cập địa chỉ:

```
http://localhost:<port>/Booking
```

---

### Bước 2 — Chọn dịch vụ

- Trang hiển thị danh sách các dịch vụ đang hoạt động.
- Sử dụng các tab **Danh mục** ở phía trên để lọc theo loại dịch vụ.
- Tích vào ô checkbox của từng dịch vụ muốn sử dụng (có thể chọn nhiều).
- Tổng tiền và thời gian dự kiến sẽ được cập nhật tự động.

---

### Bước 3 — Chọn ngày và giờ

- Chọn **ngày** từ lịch (date picker).
- Sau khi chọn ngày, hệ thống tự động tải danh sách **khung giờ trống** (8:30 – 20:30).
- Slot màu xanh = còn trống, slot màu xám = đã có người đặt.
- Click vào slot giờ muốn đặt.

> ⚠️ **Lưu ý**: Không thể chọn giờ đã qua hoặc giờ đã có lịch khác.

---

### Bước 4 — Điền thông tin cá nhân

Điền đầy đủ các trường bắt buộc:

- **Họ và tên** _(bắt buộc)_
- **Số điện thoại** _(bắt buộc)_
- **Email** _(bắt buộc — để nhận xác nhận và link hủy lịch)_
- **Ghi chú** _(tùy chọn)_

---

### Bước 5 — Xác nhận đặt lịch

- Click nút **"Đặt lịch"**.
- Hệ thống sẽ kiểm tra lại trùng lịch một lần nữa phía server.
- Nếu thành công → chuyển sang trang **"Đặt lịch thành công"**.
- **Email thông báo tiếp nhận** sẽ được gửi đến địa chỉ email đã điền.

---

### Bước 6 — Nhận email xác nhận từ Admin

- Admin sẽ xem xét và duyệt lịch.
- Khi được duyệt → bạn nhận email **"Xác nhận lịch hẹn thành công"** kèm chi tiết lịch hẹn.
- Nếu bị hủy → bạn nhận email **"Thông báo hủy lịch"**.

---

### Hủy lịch hẹn

1. Mở email xác nhận đặt lịch.
2. Click vào nút/link **"Hủy yêu cầu đặt lịch"** trong email.
3. Trang xác nhận hủy sẽ hiển thị.
4. Xác nhận hủy → hệ thống gửi email thông báo hủy thành công.

> ⚠️ Mỗi lịch hẹn chỉ có thể hủy **một lần** qua link email.

---

---

## PHẦN 2: QUẢN TRỊ VIÊN (ADMIN)

### Đăng nhập Admin Panel

Truy cập:

```
http://localhost:<port>/Account/Login
```

Dùng tài khoản:

- **Username**: `admin`
- **Password**: `admin123`

> Sau khi đăng nhập thành công, hệ thống tự động chuyển đến trang **Quản lý Lịch Đặt**.

---

### Quản lý Lịch Đặt (`/Admin/Bookings`)

**Duyệt lịch hẹn:**

1. Tìm lịch hẹn cần duyệt trong bảng danh sách.
2. Click dropdown **"Trạng thái"** → chọn `Xác nhận` (Confirmed).
3. Hệ thống tự động gửi **email xác nhận** đến khách hàng.

**Hủy lịch hẹn:**

1. Tìm lịch hẹn cần hủy.
2. Click dropdown **"Trạng thái"** → chọn `Hủy` (Cancelled).
3. Hệ thống tự động gửi **email thông báo hủy** đến khách hàng.

**Tìm kiếm & Lọc:**

- Nhập từ khóa (tên, SĐT, email) vào ô **Tìm kiếm**.
- Chọn khoảng **Từ ngày / Đến ngày**.
- Chọn **Trạng thái** để lọc.
- Click **Tìm kiếm** để áp dụng.

**Export Excel:**

1. Áp dụng bộ lọc nếu cần (hoặc để trống để export toàn bộ).
2. Click nút **"Xuất Excel"**.
3. File `.xlsx` được tải về tự động với định dạng: `DanhSachDatLich_YYYYMMDDHHSS.xlsx`

> ⚠️ **Cảnh báo chồng lịch**: Nếu có lịch hẹn bị trùng giờ, hàng tương ứng sẽ được highlight màu đỏ để cảnh báo.

---

### Quản lý Dịch Vụ (`/Admin/Services`)

**Thêm dịch vụ mới:**

1. Click **"+ Thêm dịch vụ"**.
2. Điền: Tên, Mô tả, Danh mục, Giá, Thời gian thực hiện (phút).
3. Upload ảnh đại diện (tùy chọn).
4. Click **"Lưu"**.
5. Dịch vụ mới cần được **Duyệt** trước khi hiện ra cho khách.

**Duyệt / Tắt dịch vụ:**

- Trong danh sách, click biểu tượng toggle **Duyệt** để bật/tắt hiển thị dịch vụ với khách hàng.

---

### Quản lý Danh Mục (`/Admin/Categories`)

**Thêm danh mục:**

1. Click **"+ Thêm danh mục"** → điền Tên và Mô tả → Lưu.

**Xóa danh mục:**

- Chỉ xóa được danh mục **không có dịch vụ nào liên kết**.
- Nếu còn dịch vụ → hệ thống sẽ thông báo lỗi và từ chối xóa.

---

### Quản lý Khách Hàng (`/Admin/Customers`)

- **Tạo khách hàng mới**: Điền thông tin → mật khẩu mặc định là `123456`.
- **Sửa thông tin**: Không thay đổi được mật khẩu từ màn hình này.
- **Xóa**: Xóa khách hàng khỏi hệ thống (dữ liệu lịch đặt vẫn giữ lại).

---

### Cấu hình Email SMTP (`/Admin/EmailSettings`)

Bắt buộc cấu hình để tính năng gửi email hoạt động:

| Trường       | Ví dụ                                                     |
| ------------ | --------------------------------------------------------- |
| SMTP Server  | `smtp.gmail.com`                                          |
| Port         | `587`                                                     |
| Sender Name  | `Barber Shop`                                             |
| Sender Email | `yourshop@gmail.com`                                      |
| Username     | `yourshop@gmail.com`                                      |
| Password     | App Password từ Google (không phải mật khẩu Gmail thường) |
| Enable SSL   | ✅ Bật                                                    |

> 💡 **Lưu ý Google**: Cần bật **2-Step Verification** và tạo **App Password** tại [myaccount.google.com/apppasswords](https://myaccount.google.com/apppasswords).

Sau khi điền xong → Click **"Lưu cấu hình"**. Áp dụng ngay, không cần restart.
