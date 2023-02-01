using FluentAssertions;
using Xunit;

namespace VacationRental.Domain.Test.ManagingRentals.GettingRental;

public class WhenRentalNotExist : BaseRentalsServiceFixture
{
    [Fact]
    public void ReturnsExpectedErrorResult()
    {
        WithRental(null);
        
        var result = Subject.GetRental(RentalId);
        
        result.Data.Should().Be(null);
        result.Error.Should().Be("Rental not found");
    }
}