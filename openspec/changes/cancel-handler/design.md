## Context

Hệ thống hiện đã có một phần logic hủy lịch cơ bản trong `BookingController`, tuy nhiên nó hoạt động theo cơ chế "hủy ngay lập tức" (one-click cancel) khi người dùng truy cập vào đường link từ email. Điều này không thân thiện với trải nghiệm người dùng vì không cho họ kiểm tra lại thông tin lịch hẹn trước khi xác nhận hủy chính thức.

## Goals / Non-Goals

**Goals:**

- Tạo trang giao diện xác nhận hủy lịch (Confirmation UI) hiển thị đầy đủ thông tin: Tên dịch vụ, Ngày, Giờ.
- Chuyển đổi logic hủy từ "GET thực thi ngay" sang "GET xem thông tin + POST xác nhận".
- Tích hợp thông báo Toast để phản hồi kết quả cho khách hàng một cách mượt mà.

**Non-Goals:**

- Không thay đổi cấu trúc Database (sử dụng lại `CancellationGuid` hiện có).
- Không yêu cầu người dùng đăng nhập để hủy (vẫn sử dụng link định danh duy nhất).

## Decisions

### 1. Kiến trúc luồng xử lý

- **Bước 1 (GET /Booking/Cancel/{guid})**:
  - Tìm kiếm Booking theo `Guid`.
  - Nếu tồn tại và chưa hủy: Trình bày trang UI cho phép khách hàng xác nhận.
  - Nếu đã hủy hoặc không tồn tại: Hiển thị thông báo lỗi/trạng thái tương ứng.
- **Bước 2 (POST /Booking/DoCancel)**:
  - Endpoint này sẽ nhận `Guid` qua tham số.
  - Thực hiện cập nhật `Status = Cancelled` trong DB.
  - Gửi email thông báo hủy thành công cho khách hàng.
  - Trả về kết quả JSON để FE hiển thị Toast.

### 2. Giao diện (Frontend)

- Sử dụng lại `Views/Booking/Cancel.cshtml`.
- Sử dụng **DaisyUI** và **Material Symbols** để thiết kế card thông tin lịch hẹn premium.
- Sử dụng AJAX (Fetch API) để gửi yêu cầu hủy, giúp hiển thị Toast mà không cần Reload trang nếu muốn, hoặc đơn giản hơn là Redirect về chính trang đó với tham số thành công.
- _Quyết định_: Để đơn giản và chắc chắn theo yêu cầu "Toast", con sẽ dùng Fetch API để gọi POST request và hiển thị Toast bằng JS.

### 3. Email Template

- Cập nhật link trong email gửi lúc đặt lịch: `https://domain/Booking/Cancel/[Guid]`.

## Risks / Trade-offs

- **Security**: Link hủy chứa Guid là duy nhất, tuy nhiên nếu ai đó có được email thì có thể hủy lịch. Đây là trade-off thông thường cho sự tiện lợi (không cần đăng nhập).
- **UI Consistency**: Trang này là trang Public, nên cần đảm bảo Layout đồng bộ với trang đặt lịch chính.
