using FluentValidation;
using VacationRental.Api.Models;

namespace VacationRental.Api.Validation
{
    public class RentalBindingModelValidator : AbstractValidator<RentalBindingModel>
    {
        public RentalBindingModelValidator()
        {
            RuleFor(x => x.Units)
                .GreaterThan(0)
                .WithMessage("Rental should have minimum one unit");
        }
    }
}