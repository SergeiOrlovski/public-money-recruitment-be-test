using System.Collections.Generic;
using AutoFixture;
using Moq;
using Moq.AutoMock;
using VacationRental.DataAccess.Interfaces;
using VacationRental.Domain.Interfaces;
using VacationRental.Domain.Services;
using VacationRental.Entities.Entities;
using VacationRental.Entities.Result;

namespace VacationRental.Domain.Test.ManagingRentals;

public class BaseRentalsServiceFixture
{
    protected int RentalId;
    protected AutoMocker Mocker { get; } = new AutoMocker();
    protected Fixture Fixture { get; } = new Fixture();
    protected RentalsService Subject;

    public BaseRentalsServiceFixture()
    {
        RentalId = Fixture.Create<int>();
        Subject = Mocker.CreateInstance<RentalsService>();
    }
    
    protected void WithRental(Rental rental)
    {
        Mocker
            .GetMock<IRentalsRepository>()
            .Setup(x => x.GetRental(RentalId))
            .Returns(rental);
    }

    protected void WithCreationRentalResult(bool result, Rental rental)
    {
        Mocker
            .GetMock<IRentalsRepository>()
            .Setup(x => x.CreateRental(It.IsAny<int>(), rental))
            .Returns(result);
    }
    
    protected void WithUpdatingRentalResult(bool result, Rental rental)
    {
        Mocker
            .GetMock<IRentalsRepository>()
            .Setup(x => x.UpdateRental(RentalId, rental))
            .Returns(result);
    }
    
    protected void WithRentalsCount(int count)
    {
        Mocker
            .GetMock<IRentalsRepository>()
            .Setup(x => x.GetRentalsCount())
            .Returns(count);
    }
    
    protected void WithRentalValidationResult(Result result, Rental newRental, List<Booking> existingBookings)
    {
        Mocker
            .GetMock<IRentalUpdatingValidator>()
            .Setup(x => x.IsApplicableForUpdate(newRental, existingBookings))
            .Returns(result);
    }
    
    protected void WithBookings(List<Booking> bookings)
    {
        Mocker
            .GetMock<IBookingsRepository>()
            .Setup(x => x.GetAllBookings())
            .Returns(bookings);
    }
    
    protected void WithUpdatingBookingResult(bool result, Booking booking)
    {
        Mocker
            .GetMock<IBookingsRepository>()
            .Setup(x => x.UpdateBooking(booking.Id, booking))
            .Returns(result);
    }
}