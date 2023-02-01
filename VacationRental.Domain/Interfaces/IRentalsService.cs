using VacationRental.Entities.Entities;
using VacationRental.Entities.Result;

namespace VacationRental.Domain.Interfaces;

public interface IRentalsService
{
    DataResult<Rental> GetRental(int rentalId);
    DataResult<int> CreateRental(Rental rentalBindingModel);
    DataResult<int> UpdateRental(int rentalId, Rental rentalBindingModel);
}