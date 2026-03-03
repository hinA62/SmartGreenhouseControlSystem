using FluentValidation;
using SmartGreenhouseControlSystem.Application.Commands.AddNewSensorCommand;

namespace SmartGreenhouseControlSystem.Application.Validators;

public class AddNewSensorValidator : AbstractValidator<AddNewSensorCommand>
{
    public   AddNewSensorValidator()
    {
        RuleFor(x => x.Name).NotNull().MaximumLength(50);
        RuleFor(x => x.Type).NotNull().IsInEnum();
        RuleFor(x => x.Zone).NotNull();
    }
}