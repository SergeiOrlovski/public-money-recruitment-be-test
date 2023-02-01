using System.Collections.Generic;
using VacationRental.Entities.Entities;
using VacationRental.Entities.Result;

namespace VacationRental.Domain.Interfaces;

public interface IRentalUpdatingValidator
{
    Result IsApplicableForUpdate(Rental newRental, List<Booking> existingBookings);
}