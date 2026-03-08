## 1. TailwindCSS & Layout Setup

- [ ] 1.1 Khởi tạo `package.json` và cài đặt `tailwindcss`, `postcss`, `autoprefixer` qua npm.
- [ ] 1.2 Tạo file cấu hình `tailwind.config.js` với các quy tắc scan cho `.cshtml`, `.cs`, `.html`.
- [ ] 1.3 Tạo file CSS đầu vào (`wwwroot/css/admin-input.css`) tích hợp các directive của Tailwind.
- [ ] 1.4 Thiết lập script build CSS trong `package.json` để xuất file `wwwroot/css/admin.css`.
- [ ] 1.5 Tạo mới file Layout `/Views/Shared/_AdminLayout.cshtml` sử dụng `admin.css`.

## 2. Authentication Infrastructure

- [ ] 2.1 Cài đặt NuGet package `BCrypt.Net-Next` để xử lý băm mật khẩu bảo mật.
- [ ] 2.2 Cấu hình `AddAuthentication` và `AddCookie` trong `Program.cs`.
- [ ] 2.3 Khai báo chính sách bảo mật cho các route bắt đầu bằng `/Admin`.

## 3. AccountController & Business Logic

- [ ] 3.1 Tạo `AccountController` kế thừa từ `Controller`.
- [ ] 3.2 Viết Action `Login` (GET) để hiển thị form đăng nhập.
- [ ] 3.3 Viết Action `Login` (POST) xử lý logic xác thực, kiểm tra `BCrypt.Verify` với Database.
- [ ] 3.4 Cập nhật Seed data trong `ApplicationDbContext` sử dụng mã băm thực tế thay vì giá trị giả lập.
- [ ] 3.5 Viết Action `Logout` để xóa Cookie phiên làm việc.

## 4. Views & Styling Implementation

- [ ] 4.1 Xây dựng View `/Views/Account/Login.cshtml` dựa trên thiết kế Stitch sử dụng các class Tailwind.
- [ ] 4.2 Tạo Mockup Dashboard `/Views/Admin/Index.cshtml` được bảo vệ bởi `[Authorize]`.
- [ ] 4.3 Tích hợp thông báo lỗi login vào View sử dụng Tailwind v-alert components.

## 5. Verification

- [ ] 5.1 Kiểm tra file `admin.css` được sinh ra khi build.
- [ ] 5.2 Kiểm tra việc chuyển hướng từ `/Admin/Index` về `/Account/Login` khi chưa đăng nhập.
- [ ] 5.3 Thực hiện đăng nhập thành công bằng tài khoản `admin/admin123`.
