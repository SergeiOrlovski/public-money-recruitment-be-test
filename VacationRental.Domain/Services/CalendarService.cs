using System;
using System.Collections.Generic;
using System.Linq;
using VacationRental.DataAccess.Interfaces;
using VacationRental.Domain.Interfaces;
using VacationRental.Entities.Entities;
using VacationRental.Entities.Result;

namespace VacationRental.Domain.Services;

public class CalendarService : ICalendarService
{
    private readonly IBookingsRepository _bookingsRepository;
    private readonly IRentalsRepository _rentalsRepository;

    public CalendarService(IBookingsRepository bookingsRepository, IRentalsRepository rentalsRepository)
    {
        _bookingsRepository = bookingsRepository;
        _rentalsRepository = rentalsRepository;
    }
    
    public DataResult<Calendar> GetCalendar(int rentalId, DateTime start, int nights)
    {
        if (nights < 0) return DataResult<Calendar>.Fail("Nights must be positive");
        
        var rental = _rentalsRepository.GetRental(rentalId);
        if (rental is null) return DataResult<Calendar>.Fail("Rental not found");
        
        var allExistingBookings = _bookingsRepository.GetAllBookings();
        var existingRentalBookings = allExistingBookings.Where(x => x.RentalId == rentalId).ToList();

        var result = new Calendar
        {
            RentalId = rentalId,
            Dates = new List<CalendarDate>() 
        };
        for (var i = 0; i < nights; i++)
        {
            var date = new CalendarDate
            {
                Date = start.Date.AddDays(i),
                Bookings = new List<CalendarBooking>(),
                PreparationTimes = new List<Preparation>()
            };

            foreach (var booking in existingRentalBookings)
            {
                if (booking.StartDate <= date.Date && booking.BookingEndDate > date.Date)
                {
                    date.Bookings.Add(new CalendarBooking { Id = booking.Id, Unit = booking.Unit});
                }
                if (booking.BookingEndDate <= date.Date && booking.PreparationEndDate > date.Date)
                {
                    date.PreparationTimes.Add(new Preparation { Unit = booking.Unit});
                }
            }

            result.Dates.Add(date);
        }

        return DataResult<Calendar>.Success(result);
    }
}