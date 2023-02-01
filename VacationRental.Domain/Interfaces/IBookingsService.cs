using VacationRental.Entities.Entities;
using VacationRental.Entities.Result;

namespace VacationRental.Domain.Interfaces;

public interface IBookingsService
{
    DataResult<Booking> GetBooking(int bookingId);
    DataResult<int> CreateBooking(Booking bookingBindingModel);
}