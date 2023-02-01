using System.Collections.Generic;
using System.Linq;
using VacationRental.DataAccess.Interfaces;
using VacationRental.Domain.Interfaces;
using VacationRental.Entities.Entities;
using VacationRental.Entities.Result;

namespace VacationRental.Domain.Services;

public class RentalsService : IRentalsService
{
    private readonly IRentalsRepository _rentalsRepository;
    private readonly IBookingsRepository _bookingsRepository;
    private readonly IRentalUpdatingValidator _rentalUpdatingValidator;

    public RentalsService(
        IRentalsRepository rentalsRepository,
        IBookingsRepository bookingsRepository,
        IRentalUpdatingValidator rentalUpdatingValidator)
    {
        _rentalsRepository = rentalsRepository;
        _bookingsRepository = bookingsRepository;
        _rentalUpdatingValidator = rentalUpdatingValidator;
    }

    public DataResult<Rental> GetRental(int rentalId)
    {
        var rental = _rentalsRepository.GetRental(rentalId);
        return rental is null
            ? DataResult<Rental>.Fail("Rental not found")
            : DataResult<Rental>.Success(rental);
    }

    public DataResult<int> CreateRental(Rental newRental)
    {
        var allRentalsCount = _rentalsRepository.GetRentalsCount();
        var newRentalId = allRentalsCount + 1;
        newRental.Id = newRentalId;

        var isSuccess = _rentalsRepository.CreateRental(newRentalId, newRental);

        return isSuccess
            ? DataResult<int>.Success(newRentalId)
            : DataResult<int>.Fail("Creation new rental unsuccessful");
    }

    public DataResult<int> UpdateRental(int rentalId, Rental newRental)
    {
        var existingRental = _rentalsRepository.GetRental(rentalId);
        if (existingRental is null) return DataResult<int>.Fail("Rental not found");
        
        var existingBookings = _bookingsRepository.GetAllBookings();
        var existingRentalBookings = existingBookings
            .Where(x => x.RentalId == rentalId)
            .ToList();

        var validationResult = _rentalUpdatingValidator.IsApplicableForUpdate(newRental, existingRentalBookings);
        if(validationResult.IsError) return DataResult<int>.Fail(validationResult.Error);

        existingRental.UnitsCount = newRental.UnitsCount;
        existingRental.PreparationTimeInDays = newRental.PreparationTimeInDays;

        var isSuccessful = _rentalsRepository.UpdateRental(rentalId, existingRental);
        var bookingsUpdateResult = UpdateExistingBookings(existingRentalBookings, newRental);
        
        return isSuccessful && bookingsUpdateResult
            ? DataResult<int>.Success(rentalId)
            : DataResult<int>.Fail("Update rental unsuccessful");
    }

    private bool UpdateExistingBookings(List<Booking> existingRentalBookings, Rental newRental)
    {
        if (existingRentalBookings.Count == 0) return true;
        
        var results = new List<bool>();
        foreach (var booking in existingRentalBookings)
        {
            booking.PreparationTimeInDays = newRental.PreparationTimeInDays;
            var result = _bookingsRepository.UpdateBooking(booking.Id, booking);
            results.Add(result);
        }

        return results.All(x => x);
    }
}