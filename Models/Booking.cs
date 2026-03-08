using System.ComponentModel.DataAnnotations;

namespace BarberBookingWeb.Models;

public enum BookingStatus
{
    Pending = 0,
    Confirmed = 1,
    Cancelled = 2,
    Completed = 3
}

public class Booking
{
    public Guid Id { get; set; }

    public int? CustomerId { get; set; }
    public AdminAccount? Customer { get; set; }

    [Required]
    [StringLength(200)]
    public string CustomerName { get; set; } = string.Empty;

    [Required]
    [StringLength(20)]
    public string CustomerPhone { get; set; } = string.Empty;

    [EmailAddress]
    [StringLength(100)]
    public string? CustomerEmail { get; set; }

    public string? Notes { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public BookingStatus Status { get; set; } = BookingStatus.Pending;

    [StringLength(50)]
    public string? CancellationCode { get; set; }

    public Guid? CancellationGuid { get; set; }

    [StringLength(10)]
    public string? VerificationCode { get; set; }

    // Navigation property for Many-to-Many
    public ICollection<BookingService> BookingServices { get; set; } = new List<BookingService>();
}
