# Tasks: Triển khai tính năng Xuất Excel cho Lịch đặt

## 1. Chuẩn bị và Cài đặt

- [x] 1.1 Cài đặt package NuGet `ClosedXML` vào dự án (`BarberBookingWeb.csproj`).

## 2. Xử lý Backend (Controller)

- [x] 2.1 Thêm Action `ExportBookingsToExcel` vào `AdminController.cs`.
- [x] 2.2 Viết logic truy vấn dữ liệu theo bộ lọc (Search, StartDate, EndDate, Status) tương tự Action `Bookings` nhưng không phân trang.
- [x] 2.3 Viết logic tạo file Excel bằng `ClosedXML`:
  - Tạo Workbook và Worksheet.
  - Điền Header (STT, Khách hàng, SĐT, Email, Dịch vụ, Ngày, Bắt đầu, Kết thúc, Trạng thái).
  - Điền dữ liệu từ danh sách Booking.
  - Định dạng Style (Bold cho Header, Auto-fit cột).
- [x] 2.4 Trả về `FileResult` với đúng định dạng MIME cho Excel.

## 3. Cập nhật giao diện (Frontend)

- [x] 3.1 Cập nhật `Views/Admin/Bookings.cshtml`: Thêm nút "Xuất Excel" nằm cạnh nút "Đặt lịch hộ khách".
- [x] 3.2 Đảm bảo link của nút truyền chính xác các tham số lọc hiện tại từ `@ViewBag`.
- [x] 3.3 Sử dụng icon `download` hoặc `table_view` từ Material Symbols cho nút.

## 4. Kiểm thử và Hoàn thiện

- [ ] 4.1 Kiểm tra xuất danh sách không có dữ liệu (chỉ có Header).
- [ ] 4.2 Kiểm tra xuất danh sách có áp dụng bộ lọc ngày tháng và từ khóa.
- [ ] 4.3 Kiểm tra định dạng ngày/giờ trong file Excel có chính xác không.
