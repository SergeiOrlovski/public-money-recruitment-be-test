using System;
using System.Collections.Generic;
using AutoFixture;
using FluentAssertions;
using VacationRental.Entities.Entities;
using VacationRental.Entities.Result;
using Xunit;

namespace VacationRental.Domain.Test.ManagingRentals.UpdatingRental;

public class WhenRentalApplicableForUpdate : BaseRentalsServiceFixture
{
    [Fact]
    public void ReturnsExpectedResult()
    {
        var newRental = Fixture.Build<Rental>()
            .With(x => x.UnitsCount, 2)
            .With(x => x.PreparationTimeInDays, 1)
            .Create();
        
        var existingRental = Fixture.Build<Rental>()
            .With(x => x.UnitsCount, 1)
            .With(x => x.PreparationTimeInDays, 2)
            .Create();
        
        var existBooking1 = Fixture.Build<Booking>()
            .With(x => x.Start, DateTime.Now)
            .With(x => x.Nights, 2)
            .With(x => x.PreparationTimeInDays, 1)
            .With(x => x.RentalId, RentalId)
            .Create();
        
        var existBooking2 = Fixture.Build<Booking>()
            .With(x => x.Start, DateTime.Now.AddDays(1))
            .With(x => x.Nights, 3)
            .With(x => x.PreparationTimeInDays, 1)
            .With(x => x.RentalId, RentalId)
            .Create();

        var existingBookings = new List<Booking> { existBooking1, existBooking2 };

        var validationResult = Result.Success();
        
        WithRental(existingRental);
        WithBookings(existingBookings);
        WithRentalValidationResult(validationResult, newRental, existingBookings);
        WithUpdatingRentalResult(true, existingRental);
        WithUpdatingBookingResult(true, existBooking1);
        WithUpdatingBookingResult(true, existBooking2);
        
        var result = Subject.UpdateRental(RentalId, newRental);
        
        result.Data.Should().Be(RentalId);
        result.IsError.Should().Be(false);
    }
}