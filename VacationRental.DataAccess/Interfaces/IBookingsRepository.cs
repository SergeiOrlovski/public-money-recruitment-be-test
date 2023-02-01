using System.Collections.Generic;
using VacationRental.Entities.Entities;

namespace VacationRental.DataAccess.Interfaces;

public interface IBookingsRepository
{
    List<Booking> GetAllBookings();
    Booking GetBooking(int bookingId);
    bool CreateBooking(int bookingId, Booking booking);
    bool UpdateBooking(int bookingId, Booking booking);
}