## Context

Hệ thống đặt lịch cần một bảng điều khiển cho quản trị viên (Admin Panel) để quản lý thợ và các cuộc hẹn. Hiện tại, chúng ta mới chỉ có lớp dữ liệu cơ bản. Chức năng đăng nhập là "cánh cửa" đầu tiên để vào phần quản trị.

## Goals / Non-Goals

**Goals:**

- Tạo trang đăng nhập chuyên nghiệp dành riêng cho Admin (Styling: TailwindCSS).
- Triển khai Cookie-based Authentication để duy trì trạng thái đăng nhập.
- Áp dụng các chính sách bảo mật cơ bản (Bảo vệ các Controller/Action của Admin bằng thuộc tính `[Authorize]`).
- Tích hợp TailwindCSS vào quy trình build CSS của ứng dụng.

**Non-Goals:**

- Triển khai tính năng "Quên mật khẩu" hay "Gửi OTP" qua email trong giai đoạn này.
- Phân quyền theo vai trò (Role-based Authorization) phức tạp - hiện tại chỉ mặc định quyền Admin.

## Decisions

- **Decision 1: Cookie Authentication**: Sử dụng cơ chế Authentication mặc định của ASP.NET Core thay vì JWT.
  - **Rationale**: Phù hợp cho ứng dụng Web truyền thống (MVC), đơn giản để thiết lập và bảo mật tốt cho session phía server.
- **Decision 2: TailwindCSS Integration**: Sử dụng TailwindCSS thay cho Bootstrap cho các trang Admin.
  - **Rationale**: Tạo giao diện premium theo thiết kế Stitch và linh hoạt hơn trong việc tùy chỉnh UI mà không bị bó hẹp bởi các component có sẵn của Bootstrap.
- **Decision 3: Create Separate Layout for Admin**: Tạo file `_AdminLayout.cshtml` riêng biệt.
  - **Rationale**: Để không làm ảnh hưởng đến giao diện của Khách hàng (hiện tại đang dùng Bootstrap) và đảm bảo các trang quản trị có phong cách đồng nhất.

## Risks / Trade-offs

- **[Risk] Conflicts between Bootstrap & Tailwind** → **Mitigation**: Sử dụng các file Layout riêng biệt giúp cô lập CSS, Tailwind sẽ chỉ được dùng trong trang Admin.
- **[Risk] Local database security** → **Mitigation**: Lưu mật khẩu dưới dạng Hash (BCrypt) thay vì plaintext (Seed data hiện tại cần được cập nhật).
