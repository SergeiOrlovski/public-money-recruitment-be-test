using System;
using System.Collections.Generic;
using AutoFixture;
using FluentAssertions;
using VacationRental.Entities.Entities;
using Xunit;

namespace VacationRental.Domain.Test.ManagingCalendar.GettingCalendar;

public class WhenRentalExist : BaseCalendarServiceFixture
{
    [Fact]
    public void ReturnsExpectedResult()
    {
        var rental = Fixture.Build<Rental>()
            .With(x => x.UnitsCount, 2)
            .With(x => x.PreparationTimeInDays, 1)
            .With(x => x.Id, RentalId)
            .Create();
        
        var existBooking1 = Fixture.Build<Booking>()
            .With(x => x.Start, DateTime.Now)
            .With(x => x.Nights, 1)
            .With(x => x.PreparationTimeInDays, rental.PreparationTimeInDays)
            .With(x => x.RentalId, RentalId)
            .Create();
        
        var existBooking2 = Fixture.Build<Booking>()
            .With(x => x.Start, DateTime.Now)
            .With(x => x.Nights, 2)
            .With(x => x.PreparationTimeInDays, rental.PreparationTimeInDays)
            .With(x => x.RentalId, RentalId)
            .Create();
        var existingBookings = new List<Booking> { existBooking1, existBooking2 };
        
        WithRental(rental);
        WithBookings(existingBookings);
        
        var result = Subject.GetCalendar(RentalId, DateTime.Now, 4);
        
        result.Data.Should().NotBeNull();
        result.IsError.Should().Be(false);

        result.Data.RentalId.Should().Be(RentalId);
        var dates = result.Data.Dates;
        dates.Count.Should().Be(4);

        dates[0].Date.Date.Should().Be(DateTime.Now.Date);
        dates[0].Bookings.Count.Should().Be(2);
        dates[0].Bookings.Should().Contain(x => x.Unit == existBooking1.Unit);
        dates[0].Bookings.Should().Contain(x => x.Unit == existBooking2.Unit);
        dates[0].PreparationTimes.Should().BeEmpty();

        dates[1].Date.Date.Should().Be(DateTime.Now.AddDays(1).Date);
        dates[1].Bookings.Count.Should().Be(1);
        dates[1].Bookings.Should().Contain(x => x.Unit == existBooking2.Unit);
        dates[1].PreparationTimes.Should().Contain(x => x.Unit == existBooking1.Unit);
        
        dates[2].Date.Date.Should().Be(DateTime.Now.AddDays(2).Date);
        dates[2].Bookings.Should().BeEmpty();
        dates[2].PreparationTimes.Should().Contain(x => x.Unit == existBooking2.Unit);
        
        dates[3].Date.Date.Should().Be(DateTime.Now.AddDays(3).Date);
        dates[3].Bookings.Should().BeEmpty();
        dates[3].PreparationTimes.Should().BeEmpty();
    }
}