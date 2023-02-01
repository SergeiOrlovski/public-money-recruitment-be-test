using System;
using FluentAssertions;
using Xunit;

namespace VacationRental.Domain.Test.ManagingCalendar.GettingCalendar;

public class WhenIncorrectInputParams : BaseCalendarServiceFixture
{
    [Fact]
    public void ReturnsExpectedErrorResult()
    {
        var result = Subject.GetCalendar(RentalId, new DateTime(), -1);
        
        result.Data.Should().Be(default);
        result.Error.Should().Be("Nights must be positive");
    }
}