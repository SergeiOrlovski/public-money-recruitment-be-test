using System;
using VacationRental.Entities.Entities;
using VacationRental.Entities.Result;

namespace VacationRental.Domain.Interfaces;

public interface ICalendarService
{
    DataResult<Calendar> GetCalendar(int rentalId, DateTime start, int nights);
}