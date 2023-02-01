using FluentAssertions;
using Xunit;

namespace VacationRental.Domain.Test.ManagingBookings.GettingBooking;

public class WhenBookingNotExist : BaseBookingsServiceFixture
{
    [Fact]
    public void ReturnsExpectedErrorResult()
    {
        WithBooking(null);
        
        var result = Subject.GetBooking(BookingId);
        
        result.Data.Should().Be(null);
        result.Error.Should().Be("Booking not found");
    }
}