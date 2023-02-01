using AutoFixture;
using FluentAssertions;
using VacationRental.Entities.Entities;
using Xunit;

namespace VacationRental.Domain.Test.ManagingRentals.UpdatingRental;

public class WhenRentalNotExist : BaseRentalsServiceFixture
{
    [Fact]
    public void ReturnsExpectedErrorResult()
    {
        var rental = Fixture.Build<Rental>()
            .With(x => x.UnitsCount, 2)
            .Create();
        WithRental(null);
        
        var result = Subject.UpdateRental(RentalId, rental);
        
        result.Data.Should().Be(default);
        result.Error.Should().Be("Rental not found");
    }
}