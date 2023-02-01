using AutoFixture;
using FluentAssertions;
using VacationRental.Entities.Entities;
using Xunit;

namespace VacationRental.Domain.Test.ManagingRentals.CreatingRental;

public class WhenRentalCreationFailed : BaseRentalsServiceFixture
{
    [Fact]
    public void ReturnsExpectedErrorResult()
    {
        var rental = Fixture.Build<Rental>()
            .With(x => x.UnitsCount, 2)
            .Create();
        WithRentalsCount(1);
        WithCreationRentalResult(false, rental);
        
        var result = Subject.CreateRental(rental);
        
        result.Data.Should().Be(default);
        result.Error.Should().Be("Creation new rental unsuccessful");
    }
}