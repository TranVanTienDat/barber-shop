## ADDED Requirements

### Requirement: TailwindCSS Development Environment

Dự án SHALL tích hợp công cụ TailwindCSS CLI hoặc PostCSS để biên dịch các class tiện ích thành file CSS.

#### Scenario: CSS compilation

- **WHEN** nhà phát triển thay đổi mã nguồn HTML/CSHTML và chạy lệnh build
- **THEN** file `wwwroot/css/admin.css` phải được cập nhật chứa các class Tailwind tương ứng.

### Requirement: Admin Layout Styling

Trang quản trị SHALL sử dụng file Layout riêng biệt (`_AdminLayout.cshtml`) đã được liên kết với file Tailwind CSS đã biên dịch.

#### Scenario: Verify styling

- **WHEN** người dùng xem trang đăng nhập Admin
- **THEN** giao diện phải hiển thị đúng các style của Tailwind (ví dụ: Rounded corners, Hover effects, Tailwind colors).
