# Design: Xuất Excel cho danh sách đặt lịch

## Context

Hệ thống hiện tại có trang Quản lý lịch đặt (`/Admin/Bookings`) cho phép tìm kiếm và lọc dữ liệu. Tuy nhiên, dữ liệu này chỉ hiển thị trên web. Thiết kế này hướng tới việc tái sử dụng logic lọc hiện có để tạo ra một file Excel phản ánh đúng kết quả tìm kiếm của người dùng.

## Goals / Non-Goals

**Goals:**

- Xuất file Excel (.xlsx) chứa toàn bộ kết quả lọc (không phân trang).
- Đảm bảo định dạng Excel dễ đọc: Có tiêu đề cột, tự động căn chỉnh độ rộng, định dạng ngày tháng tiếng Việt.
- Tích hợp nút Xuất Excel mượt mà vào giao diện hiện tại.
- Xử lý các trường hợp không có dữ liệu.

**Non-Goals:**

- Không hỗ trợ xuất ra các định dạng khác như CSV, PDF (trong phạm vi task này).
- Không hỗ trợ tùy chỉnh các cột cần xuất từ phía người dùng (fixed columns).
- Không thay đổi cấu trúc Database.

## Decisions

1.  **Thư viện**: Sử dụng `ClosedXML` vì sự ổn định, dễ sử dụng và hỗ trợ tốt định dạng `.xlsx` nguyên bản.
2.  **API Flow**:
    - Người dùng nhấn nút "Xuất Excel".
    - Gửi request GET tới `/Admin/ExportBookingsToExcel` kèm các query params lọc.
    - Server truy vấn dữ liệu từ DB (không skip/take), tạo file Excel trong bộ nhớ (MemoryStream).
    - Trả về file với Content-Type: `application/vnd.openxmlformats-officedocument.spreadsheetml.sheet`.
3.  **Cấu trúc File Excel**:
    - Cột: STT | Khách hàng | Điện thoại | Email | Dịch vụ | Ngày | Bắt đầu | Kết thúc | Trạng thái.
    - Header: In đậm, nền xám nhạt để phân biệt.
    - Kiểu dữ liệu: Ngày tháng sử dụng format `dd/MM/yyyy`, giờ sử dụng `HH:mm`.

## Risks / Trade-offs

- **Hiệu năng**: Nếu số lượng bản ghi cực lớn (hàng chục nghìn), việc xuất trực tiếp có thể gây chậm server. Tuy nhiên, với salon quy mô vừa và nhỏ, giải pháp này là tối ưu nhất vì sự đơn giản và đáp ứng nhanh.
- **Dependency**: Thêm thư viện `ClosedXML` làm tăng kích thước ứng dụng một chút, nhưng đáng giá cho tính năng này.
