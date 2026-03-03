using FluentValidation;
using SmartGreenhouseControlSystem.Application.Commands.AddNewDeviceCommand;

namespace SmartGreenhouseControlSystem.Application.Validators;

public class AddNewDeviceValidator : AbstractValidator<AddNewDeviceCommand>
{
    public AddNewDeviceValidator()
    {
        RuleFor(x => x.Name).NotNull().MaximumLength(50);
        RuleFor(x => x.SystemId).NotNull();
    }
}