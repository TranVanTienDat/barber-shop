using System.ComponentModel.DataAnnotations;

namespace BarberBookingWeb.Models;

public class BarberService
{
    public int Id { get; set; }

    [Display(Name = "Danh mục")]
    public int CategoryId { get; set; }

    [Display(Name = "Danh mục")]
    public virtual Category? Category { get; set; }

    [Required(ErrorMessage = "Vui lòng chọn danh mục")]
    [StringLength(200)]
    public string CategoryName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập tên dịch vụ")]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập mô tả")]
    public string Description { get; set; } = string.Empty;

    [StringLength(500)]
    public string? ImageUrl { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập thời gian thực hiện")]
    [Range(1, 480, ErrorMessage = "Thời gian từ 1 đến 480 phút")]
    public int DurationInMinutes { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập giá tiền")]
    [Range(0, 10000000, ErrorMessage = "Giá tiền không hợp lệ")]
    public decimal Price { get; set; }

    public bool IsApproved { get; set; } = true;

    // Navigation property for Many-to-Many
    public ICollection<BookingService> BookingServices { get; set; } = new List<BookingService>();
}
