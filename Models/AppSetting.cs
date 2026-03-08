using System.ComponentModel.DataAnnotations;

namespace BarberBookingWeb.Models;

public class AppSetting
{
    [Key]
    public string Key { get; set; } = string.Empty;
    public string? Value { get; set; }
    public string? Description { get; set; }
}
