using System;
using System.Collections.Generic;
using System.Linq;
using VacationRental.DataAccess.Interfaces;
using VacationRental.Domain.Interfaces;
using VacationRental.Entities.Entities;
using VacationRental.Entities.Result;

namespace VacationRental.Domain.Services;

public class BookingsService : IBookingsService
{
    private readonly IBookingsRepository _bookingsRepository;
    private readonly IRentalsRepository _rentalsRepository;

    public BookingsService(IBookingsRepository bookingsRepository, IRentalsRepository rentalsRepository)
    {
        _bookingsRepository = bookingsRepository;
        _rentalsRepository = rentalsRepository;
    }

    public DataResult<Booking> GetBooking(int bookingId)
    {
        var booking = _bookingsRepository.GetBooking(bookingId);
        return booking is null
            ? DataResult<Booking>.Fail("Booking not found")
            : DataResult<Booking>.Success(booking);
    }

    public DataResult<int> CreateBooking(Booking newBooking)
    { 
        var rental = _rentalsRepository.GetRental(newBooking.RentalId);
        if (rental is null) return DataResult<int>.Fail("Rental not found");
        
        var existingBookings = _bookingsRepository.GetAllBookings();
        var bookedUnits = GetBookedUnits(existingBookings, newBooking);
        if(bookedUnits.Length >= rental.UnitsCount) return DataResult<int>.Fail("No more available units");

        var newBookingKey = existingBookings.Count + 1;
        newBooking.Id = newBookingKey;
        newBooking.PreparationTimeInDays = rental.PreparationTimeInDays;
        newBooking.Unit = GetAvailableUnit(bookedUnits, rental);
        
        var isSuccess = _bookingsRepository.CreateBooking(newBookingKey, newBooking);

        return isSuccess
            ? DataResult<int>.Success(newBookingKey)
            : DataResult<int>.Fail("Booking of rental unsuccessful");
    }

    private int GetAvailableUnit(int[] bookedUnits, Rental rental)
    
    {
        if (bookedUnits.Length == 0) return 1;
        var availableUnits = rental.Units.Except(bookedUnits);

        return availableUnits.FirstOrDefault();
    }

    private int[] GetBookedUnits(List<Booking> existingBookings, Booking model)
    {
        var existingRentalBooking = existingBookings.Where(x => x.RentalId == model.RentalId).ToList();
        if (existingRentalBooking.Count == 0) return Array.Empty<int>();
        
        return existingRentalBooking
            .Where(booking => 
                (booking.StartDate <= model.StartDate && booking.PreparationEndDate > model.StartDate) || 
                (booking.StartDate < model.PreparationEndDate && booking.PreparationEndDate >= model.PreparationEndDate) ||
                (booking.StartDate > model.StartDate && booking.PreparationEndDate < model.PreparationEndDate))
            .Select(x => x.Unit)
            .ToArray();
    }
}