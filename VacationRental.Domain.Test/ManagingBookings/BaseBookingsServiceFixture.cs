using System.Collections.Generic;
using AutoFixture;
using Moq;
using Moq.AutoMock;
using VacationRental.DataAccess.Interfaces;
using VacationRental.Domain.Services;
using VacationRental.Entities.Entities;

namespace VacationRental.Domain.Test.ManagingBookings;

public class BaseBookingsServiceFixture
{
    protected int BookingId;
    protected AutoMocker Mocker { get; } = new AutoMocker();
    protected Fixture Fixture { get; } = new Fixture();
    protected BookingsService Subject;

    public BaseBookingsServiceFixture()
    {
        BookingId = Fixture.Create<int>();
        Subject = Mocker.CreateInstance<BookingsService>();
    }
    
    protected void WithBooking(Booking booking)
    {
        Mocker
            .GetMock<IBookingsRepository>()
            .Setup(x => x.GetBooking(BookingId))
            .Returns(booking);
    }
    
    protected void WithBookings(List<Booking> bookings)
    {
        Mocker
            .GetMock<IBookingsRepository>()
            .Setup(x => x.GetAllBookings())
            .Returns(bookings);
    }
    
    protected void WithCreationBookingResult(bool result, Booking booking)
    {
        Mocker
            .GetMock<IBookingsRepository>()
            .Setup(x => x.CreateBooking(It.IsAny<int>(), booking))
            .Returns(result);
    }

    protected void WithRental(int rentalId, Rental rental)
    {
        Mocker
            .GetMock<IRentalsRepository>()
            .Setup(x => x.GetRental(rentalId))
            .Returns(rental);
    }
}