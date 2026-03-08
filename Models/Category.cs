using System.ComponentModel.DataAnnotations;

namespace BarberBookingWeb.Models;

public class Category
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Tên danh mục không được để trống")]
    [StringLength(100)]
    [Display(Name = "Tên danh mục")]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Mô tả")]
    public string? Description { get; set; }

    // Navigation property
    public virtual ICollection<BarberService> BarberServices { get; set; } = new List<BarberService>();
}
