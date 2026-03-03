using FluentValidation;
using SmartGreenhouseControlSystem.Application.Commands.AddNewDeviceCommand;

namespace SmartGreenhouseControlSystem.Application.Validators;

public class AddNewDeviceValidator : AbstractValidator<AddNewDeviceCommand>
{
    public AddNewDeviceValidator()
    {
        RuleFor(x => x.Name).NotNull().MaximumLength(50)
            .WithMessage("Name required and must be less than 50 characters.");
        
        RuleFor(x => x.SystemId).NotNull().WithMessage("System ID is required.");
        
        RuleFor(x => x.SensorIds).NotNull().WithMessage("Sensor IDs are required.");
    }
}