using System;
using System.Collections.Generic;
using AutoFixture;
using FluentAssertions;
using VacationRental.Entities.Entities;
using Xunit;

namespace VacationRental.Domain.Test.ManagingBookings.CreatingBooking;

public class WhenNewBookingCreationFailed : BaseBookingsServiceFixture
{
    [Fact]
    public void ReturnsExpectedErrorResult()
    {
        var rental = Fixture.Build<Rental>()
            .With(x => x.UnitsCount, 2)
            .Create();
        
        var newBooking = Fixture.Build<Booking>()
            .With(x => x.Start, DateTime.Now)
            .With(x => x.Nights, 2)
            .With(x => x.PreparationTimeInDays, 1)
            .Create();
        
        var existBooking1 = Fixture.Build<Booking>()
            .With(x => x.Start, DateTime.Now)
            .With(x => x.Nights, 2)
            .With(x => x.PreparationTimeInDays, 1)
            .With(x => x.RentalId, newBooking.RentalId)
            .Create();
        var existingBookings = new List<Booking> { existBooking1 };

        WithRental(newBooking.RentalId, rental);
        WithBookings(existingBookings);
        WithCreationBookingResult(false, newBooking);
        
        var result = Subject.CreateBooking(newBooking);
        
        result.Data.Should().Be(default);
        result.Error.Should().Be("Booking of rental unsuccessful");
    }
}