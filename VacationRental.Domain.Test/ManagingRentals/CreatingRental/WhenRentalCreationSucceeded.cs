using AutoFixture;
using FluentAssertions;
using VacationRental.Entities.Entities;
using Xunit;

namespace VacationRental.Domain.Test.ManagingRentals.CreatingRental;

public class WhenRentalCreationSucceeded : BaseRentalsServiceFixture
{
    [Fact]
    public void ReturnsExpectedResult()
    {
        var rental = Fixture.Build<Rental>()
            .With(x => x.UnitsCount, 2)
            .Create();
        WithRentalsCount(1);
        WithCreationRentalResult(true, rental);
        
        var result = Subject.CreateRental(rental);
        
        result.Data.Should().Be(rental.Id);
        result.IsError.Should().Be(false);
    }
}