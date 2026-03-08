## 1. Backend: Cập nhật API xử lý hủy

- [x] 1.1 Cập nhật `BookingController.cs`: Thay đổi action `Cancel(Guid id)` (GET) để chỉ trả về View hiển thị thông tin thay vì thực hiện hủy ngay lập tức.
- [x] 1.2 Tạo mới action `DoCancel(Guid id)` (POST) trong `BookingController.cs`:
  - Thực hiện cập nhật trạng thái `BookingStatus.Cancelled`.
  - Gửi email thông báo hủy thành công cho khách hàng.
  - Trả về kết quả JSON báo lỗi hoặc thành công.

## 2. Frontend: Nâng cấp giao diện hủy lịch

- [x] 2.1 Cập nhật `Views/Booking/Cancel.cshtml`:
  - Thiết kế lại giao diện Card thông tin: Tên dịch vụ, Ngày, Giờ (sử dụng DaisyUI).
  - Thêm nút "Xác nhận hủy lịch" với hiệu ứng loading.
- [x] 2.2 Viết script xử lý sự kiện click nút hủy:
  - Sử dụng `fetch` để gọi POST API `DoCancel`.
  - Hiển thị Toast thông báo kết quả (Sử dụng Toast của hệ thống hoặc DaisyUI).
  - Chuyển hướng hoặc cập nhật UI sau khi hủy thành công.

## 3. Email & Template

- [x] 3.1 Cập nhật logic gửi email trong `BookingController.Confirm`: Đảm bảo link hủy được định dạng đúng.
- [x] 3.2 Kiểm tra lại template email xác nhận để chắc chắn có hướng dẫn hủy lịch cho khách hàng.

## 4. Kiểm thử (Verification)

- [ ] 4.1 Đặt lịch thử và kiểm tra email có link hủy.
- [ ] 4.2 Truy cập link hủy, kiểm tra thông tin hiển thị chính xác.
- [ ] 4.3 Thực hiện xác nhận hủy và kiểm tra Toast thông báo + trạng thái trong Database.
