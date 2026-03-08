using System.ComponentModel.DataAnnotations;

namespace BarberBookingWeb.Models;

public class AdminAccount
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Username { get; set; } = string.Empty;

    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    [StringLength(150)]
    public string? FullName { get; set; }

    [StringLength(20)]
    public string? PhoneNumber { get; set; }

    [EmailAddress]
    [StringLength(100)]
    public string? Email { get; set; }

    public DateTime RegisteredDate { get; set; } = DateTime.UtcNow;

    [StringLength(20)]
    public string Role { get; set; } = "Customer"; // Default role
}
