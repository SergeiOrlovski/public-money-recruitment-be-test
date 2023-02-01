using System;
using System.Collections.Generic;

namespace VacationRental.Entities.Entities;

public class CalendarDate
{
    public DateTime Date { get; set; }
    public List<CalendarBooking> Bookings { get; set; }
    public List<Preparation> PreparationTimes { get; set; }
}