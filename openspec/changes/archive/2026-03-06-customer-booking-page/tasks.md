## 1. Khởi tạo Cấu trúc & Backend cơ bản

- [x] 1.1 Tạo `BookingController` với các Action: `Index` (Trang đặt lịch), `Confirm` (Xử lý đặt), `Success` (Thành công), `Cancel` (Hủy lịch).
- [x] 1.2 Tạo `IEmailService` và triển khai `SimpleEmailService` (ghi log thông báo).
- [x] 1.3 Cập nhật `Booking` Model: Thêm cột `CancellationGuid` (Guid) để định danh link hủy bảo mật.
- [x] 1.4 Chạy Migration để cập nhật database (nếu cần đổi Model).

## 2. Giao diện Đặt lịch (Public Booking)

- [x] 2.1 Xây dựng View `Booking/Index.cshtml` sử dụng Tailwind CSS (Stitch style).
- [x] 2.2 Đổ dữ liệu Dịch vụ đã duyệt (`IsApproved`) lên giao diện.
- [x] 2.3 Cài đặt thư viện Calendar (hoặc viết custom logic) để chọn ngày.
- [x] 2.4 Viết API Endpoint trả về danh sách khung giờ trống dựa trên ngày đã chọn.

## 3. Logic Đặt lịch & Validation

- [x] 3.1 Viết JavaScript xử lý chọn nhiều dịch vụ, tính tổng tiền và tổng thời gian.
- [x] 3.2 Implement logic kiểm tra trùng lịch phía Server-side trong Action `Confirm`.
- [x] 3.3 Đảm bảo lưu đúng quan hệ Many-to-Many giữa `Booking` và `BarberServices`.

## 4. Thông báo & Hủy lịch

- [x] 4.1 Tích hợp gửi email sau khi lưu `Booking` thành công (Chứa mã đặt lịch và link hủy).
- [x] 4.2 Xây dựng Action `Cancel` trong `BookingController` để xử lý yêu cầu hủy qua `CancellationGuid`.
- [x] 4.3 Tạo trang thông báo hủy thành công.

## 5. Kiểm thử & Hoàn thiện

- [x] 5.1 Test luồng đặt lịch hoàn chỉnh (Happy path).
- [x] 5.2 Test trường hợp trùng lịch (Cùng đặt một lúc).
- [x] 5.3 Test luồng hủy lịch qua link email.
