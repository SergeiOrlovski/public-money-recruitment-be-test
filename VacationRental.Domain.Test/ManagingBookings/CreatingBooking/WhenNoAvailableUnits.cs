using System;
using System.Collections.Generic;
using AutoFixture;
using FluentAssertions;
using VacationRental.Entities.Entities;
using Xunit;

namespace VacationRental.Domain.Test.ManagingBookings.CreatingBooking;

public class WhenNoAvailableUnits : BaseBookingsServiceFixture
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
        
        var existBooking2 = Fixture.Build<Booking>()
            .With(x => x.Start, DateTime.Now.AddDays(1))
            .With(x => x.Nights, 3)
            .With(x => x.PreparationTimeInDays, 1)
            .With(x => x.RentalId, newBooking.RentalId)
            .Create();

        WithRental(newBooking.RentalId, rental);
        WithBookings(new List<Booking>{ existBooking1, existBooking2 });
        
        var result = Subject.CreateBooking(newBooking);
        
        result.Data.Should().Be(default);
        result.Error.Should().Be("No more available units");
    }
}