using System.Collections.Generic;
using System.Linq;
using VacationRental.Domain.Interfaces;
using VacationRental.Entities.Entities;
using VacationRental.Entities.Result;

namespace VacationRental.Domain.Validators;

public class RentalUpdatingValidator : IRentalUpdatingValidator
{
    public Result IsApplicableForUpdate(Rental newRental, List<Booking> existingRentalBooking)
    {
        if(existingRentalBooking.Count == 0) return Result.Success();
        
        var bookedUnits = existingRentalBooking
            .Select(x => x.Unit)
            .Distinct()
            .ToList();
        if (bookedUnits.Count > newRental.UnitsCount) return Result.Fail("Units already booked");

        var isOverlapBookingExist = TryDetectOverlapping(
            existingRentalBooking,
            bookedUnits,
            newRental.PreparationTimeInDays);
        if (isOverlapBookingExist) return Result.Fail("Detected overlapping between existing bookings");
        
        return Result.Success();
    }
    
    private bool TryDetectOverlapping(
        List<Booking> existingRentalBooking,
        IEnumerable<int> bookedUnits,
        int newPreparationTime)
    {
        var isOverlapBooking = false;
        foreach (var unit in bookedUnits)
        {
            var existingBookedUnits = existingRentalBooking
                .Where(x => x.Unit == unit)
                .OrderBy(s => s.StartDate)
                .ToList();

            if (existingBookedUnits.Count <= 1) continue;

            var overlappingBookings = existingBookedUnits
                .SelectMany((firstBooking, i) => existingBookedUnits
                .Skip(i + 1)
                .Where(secondBooking => DoesOverlap(firstBooking, secondBooking, newPreparationTime)));

            if (overlappingBookings.Any())
            {
                isOverlapBooking = true;
                break;
            }
        }

        return isOverlapBooking;
    }

    private bool DoesOverlap(Booking firstBooking, Booking secondBooking, int newPreparationTime) 
        => firstBooking.BookingEndDate.AddDays(newPreparationTime) >= secondBooking.StartDate;
}