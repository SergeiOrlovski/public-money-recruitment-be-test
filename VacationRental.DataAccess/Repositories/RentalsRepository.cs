using System.Collections.Generic;
using VacationRental.DataAccess.Interfaces;
using VacationRental.Entities.Entities;

namespace VacationRental.DataAccess.Repositories;

public class RentalsRepository : BaseRepository<Rental>, IRentalsRepository
{
    public RentalsRepository(IDictionary<int, Rental> dataSource) : base(dataSource) {}

    public int GetRentalsCount() => GetCountValues();
    public Rental GetRental(int rentalId) => GetValue(rentalId);
    public bool CreateRental(int rentalId, Rental rental) => CreateValue(rentalId, rental);
    public bool UpdateRental(int rentalId, Rental rental) => UpdateValue(rentalId, rental);
}