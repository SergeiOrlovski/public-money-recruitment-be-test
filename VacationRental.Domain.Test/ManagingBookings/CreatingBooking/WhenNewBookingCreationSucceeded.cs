using System;
using System.Collections.Generic;
using AutoFixture;
using FluentAssertions;
using Moq;
using VacationRental.DataAccess.Interfaces;
using VacationRental.Entities.Entities;
using Xunit;

namespace VacationRental.Domain.Test.ManagingBookings.CreatingBooking;

public class WhenNewBookingCreationSucceeded : BaseBookingsServiceFixture
{
    [Fact]
    public void ReturnsExpectedResult()
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
        WithCreationBookingResult(true, newBooking);
        
        var result = Subject.CreateBooking(newBooking);
        
        result.Data.Should().Be(newBooking.Id);
        result.IsError.Should().Be(false);
        
        Mocker
            .GetMock<IBookingsRepository>()
            .Setup(x => x.CreateBooking(It.Is<int>(x => x == existingBookings.Count + 1), newBooking));
    }
    
    [Fact]
    public void ExpectedBookingIdApplied()
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
        WithCreationBookingResult(true, newBooking);
        
        var result = Subject.CreateBooking(newBooking);

        Mocker
            .GetMock<IBookingsRepository>()
            .Setup(x => x.CreateBooking(It.Is<int>(x => x == existingBookings.Count + 1), newBooking));
    }
}