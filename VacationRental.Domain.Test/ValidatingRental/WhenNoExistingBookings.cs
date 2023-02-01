using System;
using System.Collections.Generic;
using AutoFixture;
using FluentAssertions;
using VacationRental.Entities.Entities;
using Xunit;

namespace VacationRental.Domain.Test.ValidatingRental;

public class WhenNoExistingBookings : BaseRentalUpdatingValidatorFixture
{
    [Fact]
    public void ReturnsExpectedResult()
    {
        var rental = Fixture.Create<Rental>();

        var existingBookings = new List<Booking>();

        var result = Subject.IsApplicableForUpdate(rental, existingBookings);

        result.IsError.Should().BeFalse();
    }
}