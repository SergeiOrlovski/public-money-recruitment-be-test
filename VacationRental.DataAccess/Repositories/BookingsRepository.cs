using System.Collections.Generic;
using VacationRental.DataAccess.Interfaces;
using VacationRental.Entities.Entities;

namespace VacationRental.DataAccess.Repositories;

public class BookingsRepository : BaseRepository<Booking>, IBookingsRepository
{
    public BookingsRepository(IDictionary<int, Booking> dataSource) : base(dataSource) { }

    public List<Booking> GetAllBookings() => GetAllValues();
    public Booking GetBooking(int bookingId) => GetValue(bookingId);
    public bool CreateBooking(int bookingId, Booking booking) => CreateValue(bookingId, booking);
    public bool UpdateBooking(int bookingId, Booking booking) => UpdateValue(bookingId, booking);
}