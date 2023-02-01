using System;
using FluentAssertions;
using Xunit;

namespace VacationRental.Domain.Test.ManagingCalendar.GettingCalendar;

public class WhenRentalNotExist : BaseCalendarServiceFixture
{
    [Fact]
    public void ReturnsExpectedErrorResult()
    {
        WithRental(null);
        var result = Subject.GetCalendar(RentalId, new DateTime(), 2);
        
        result.Data.Should().Be(default);
        result.Error.Should().Be("Rental not found");
    }
}