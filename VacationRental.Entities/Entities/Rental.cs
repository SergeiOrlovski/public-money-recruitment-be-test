using System.Collections.Generic;
using System.Linq;

namespace VacationRental.Entities.Entities;

public class Rental
{
    public int Id { get; set; }
    public int UnitsCount { get; set; }
    public int PreparationTimeInDays { get; set; }

    public IEnumerable<int> Units => Enumerable.Range(1, UnitsCount);
}