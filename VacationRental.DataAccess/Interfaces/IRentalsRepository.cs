using VacationRental.Entities.Entities;

namespace VacationRental.DataAccess.Interfaces;

public interface IRentalsRepository
{
    int GetRentalsCount();
    Rental GetRental(int rentalId);
    bool CreateRental(int rentalId, Rental rental);
    bool UpdateRental(int rentalId, Rental rental);
}