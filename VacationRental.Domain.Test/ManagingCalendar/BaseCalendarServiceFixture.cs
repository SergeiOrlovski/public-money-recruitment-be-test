using System.Collections.Generic;
using AutoFixture;
using Moq;
using Moq.AutoMock;
using VacationRental.DataAccess.Interfaces;
using VacationRental.Domain.Interfaces;
using VacationRental.Domain.Services;
using VacationRental.Entities.Entities;
using VacationRental.Entities.Result;

namespace VacationRental.Domain.Test.ManagingCalendar;

public class BaseCalendarServiceFixture
{
    protected int RentalId;
    protected AutoMocker Mocker { get; } = new AutoMocker();
    protected Fixture Fixture { get; } = new Fixture();
    protected CalendarService Subject;

    public BaseCalendarServiceFixture()
    {
        RentalId = Fixture.Create<int>();
        Subject = Mocker.CreateInstance<CalendarService>();
    }
    
    protected void WithRental(Rental rental)
    {
        Mocker
            .GetMock<IRentalsRepository>()
            .Setup(x => x.GetRental(RentalId))
            .Returns(rental);
    }

    protected void WithBookings(List<Booking> bookings)
    {
        Mocker
            .GetMock<IBookingsRepository>()
            .Setup(x => x.GetAllBookings())
            .Returns(bookings);
    }
}