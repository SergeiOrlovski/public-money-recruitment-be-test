using AutoFixture;
using FluentAssertions;
using VacationRental.Entities.Entities;
using Xunit;

namespace VacationRental.Domain.Test.ManagingBookings.GettingBooking;

public class WhenBookingExist : BaseBookingsServiceFixture
{
    [Fact]
    public void ReturnsExpectedValue()
    {
        var booking = Fixture.Create<Booking>();
        WithBooking(booking);
        
        var result = Subject.GetBooking(BookingId);
        
        result.Data.Should().Be(booking);
        result.IsError.Should().Be(false);
    }
}