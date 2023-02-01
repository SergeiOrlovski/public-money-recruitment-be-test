using System;
using System.Collections.Generic;
using AutoFixture;
using FluentAssertions;
using VacationRental.Entities.Entities;
using Xunit;

namespace VacationRental.Domain.Test.ValidatingRental;

public class WhenDetectedOverlapWithNewPreparationTime : BaseRentalUpdatingValidatorFixture
{
    [Fact]
    public void ReturnsExpectedErrorResult()
    {
        var rental = Fixture.Build<Rental>()
            .With(x => x.UnitsCount, 1)
            .With(x => x.PreparationTimeInDays, 1)
            .Create();

        var existBooking1 = Fixture.Build<Booking>()
            .With(x => x.Start, DateTime.Now)
            .With(x => x.Nights, 1)
            .With(x => x.PreparationTimeInDays, rental.PreparationTimeInDays)
            .With(x => x.RentalId, rental.Id)
            .With(x => x.Unit, 1)
            .Create();
        
        var existBooking2 = Fixture.Build<Booking>()
            .With(x => x.Start, DateTime.Now.AddDays(2))
            .With(x => x.Nights, 3)
            .With(x => x.PreparationTimeInDays, rental.PreparationTimeInDays)
            .With(x => x.RentalId, rental.Id)
            .With(x => x.Unit, 1)
            .Create();

        var existingBookings = new List<Booking> { existBooking1, existBooking2 };

        var result = Subject.IsApplicableForUpdate(rental, existingBookings);

        result.IsError.Should().BeTrue();
        result.Error.Should().Be("Detected overlapping between existing bookings");
    }
}