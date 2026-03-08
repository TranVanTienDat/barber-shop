namespace BarberBookingWeb.Models;

public class BookingService
{
    public Guid BookingId { get; set; }
    public Booking Booking { get; set; } = null!;

    public int ServiceId { get; set; }
    public BarberService BarberService { get; set; } = null!;
}
