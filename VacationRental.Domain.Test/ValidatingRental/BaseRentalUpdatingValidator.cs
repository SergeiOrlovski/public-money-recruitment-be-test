using AutoFixture;
using Moq.AutoMock;
using VacationRental.Domain.Validators;

namespace VacationRental.Domain.Test.ValidatingRental;

public class BaseRentalUpdatingValidatorFixture
{
    protected AutoMocker Mocker { get; } = new AutoMocker();
    protected Fixture Fixture { get; } = new Fixture();
    protected RentalUpdatingValidator Subject;

    public BaseRentalUpdatingValidatorFixture()
    {
        Subject = Mocker.CreateInstance<RentalUpdatingValidator>();
    }
}