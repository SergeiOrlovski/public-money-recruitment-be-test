using AutoFixture;
using FluentAssertions;
using VacationRental.Entities.Entities;
using Xunit;

namespace VacationRental.Domain.Test.ManagingRentals.GettingRental;

public class WhenRentalExist : BaseRentalsServiceFixture
{
    [Fact]
    public void ReturnsExpectedResult()
    {
        var rental = Fixture.Create<Rental>();
        WithRental(rental);
        
        var result = Subject.GetRental(RentalId);
        
        result.Data.Should().Be(rental);
        result.IsError.Should().Be(false);
    }
}