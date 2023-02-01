using System;

namespace VacationRental.Entities.Entities;

public class Booking
{
    public int Id { get; set; }
    public int RentalId { get; set; }
    public int Unit { get; set; }
    public DateTime Start { get; set; }
    public int Nights { get; set; }
    public int PreparationTimeInDays { get; set; }

    public DateTime StartDate => Start.Date;
    public DateTime BookingEndDate => Start.Date.AddDays(Nights);
    public DateTime PreparationEndDate => BookingEndDate.AddDays(PreparationTimeInDays);
}