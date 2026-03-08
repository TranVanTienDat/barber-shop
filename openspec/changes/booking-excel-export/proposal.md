# Proposal: Xuất Excel cho danh sách đặt lịch (Booking Excel Export)

## Problem

Hiện tại, Quản trị viên chỉ có thể xem danh sách đặt lịch trên giao diện web. Việc tổng hợp dữ liệu để báo cáo hàng tháng hoặc xử lý ngoại tuyến (offline) gặp khó khăn do thiếu tính năng xuất dữ liệu ra định dạng tệp tin phổ biến như Excel.

## Objective

Cài đặt tính năng xuất danh sách đặt lịch ra file Excel (.xlsx) giúp Quản trị viên:

- Dễ dàng quản lý và báo cáo dữ liệu.
- Lọc và xuất đúng dữ liệu đang hiển thị trên web (bao gồm tìm kiếm, lọc theo ngày, trạng thái).
- Cung cấp file Excel chuyên nghiệp với đầy đủ thông tin cần thiết.

## What Changes

### New Capabilities

- `booking-excel-export`: Cung cấp logic tạo file Excel từ danh sách Booking. Hỗ trợ định dạng cột, tiêu đề và xuất dữ liệu an toàn.

### Modified Capabilities

- Không có. (Yêu cầu này tập trung vào việc bổ sung tính năng mới cho hệ thống quản trị hiện tại).

## Impact

1.  **Dependencies**: Bổ sung thư viện `ClosedXML` vào project (`BarberBookingWeb.csproj`).
2.  **Backend**:
    - Thêm Action `ExportBookingsToExcel` vào `AdminController`.
    - Logic lọc dữ liệu tương đồng với Action `Bookings` hiện tại.
3.  **Frontend**:
    - Cập nhật `Views/Admin/Bookings.cshtml`: Thêm nút "Xuất Excel" (màu thành công - xanh lá) vào khu vực Header.
    - Cập nhật link cho nút Xuất Excel để truyền đầy đủ các tham số lọc hiện tại.
4.  **Database**: Không thay đổi schema, chỉ đọc dữ liệu.
