using AutoFixture;
using FluentAssertions;
using VacationRental.Entities.Entities;
using Xunit;

namespace VacationRental.Domain.Test.ManagingBookings.CreatingBooking;

public class WhenRentalNotExist : BaseBookingsServiceFixture
{
    [Fact]
    public void ReturnsExpectedErrorResult()
    {
        var booking = Fixture.Create<Booking>();
        WithRental(booking.RentalId, null);
        
        var result = Subject.CreateBooking(booking);
        
        result.Data.Should().Be(default);
        result.Error.Should().Be("Rental not found");
    }
}