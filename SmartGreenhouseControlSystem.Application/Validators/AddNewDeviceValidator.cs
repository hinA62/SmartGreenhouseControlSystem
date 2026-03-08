using FluentValidation;
using SmartGreenhouseControlSystem.Application.Commands.AddNewDeviceCommand;

namespace SmartGreenhouseControlSystem.Application.Validators;

public class AddNewDeviceValidator : AbstractValidator<AddNewDeviceCommand>
{
    public AddNewDeviceValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.")
            .MaximumLength(50).WithMessage("Name must be less than 50 characters.");
        
        RuleFor(x => x.SystemId).NotEmpty().WithMessage("System ID is required.");
        
        //to review: do we need to validate that the sensors exist?
        RuleFor(x => x.SensorIds).NotNull().WithMessage("Sensor IDs are required.");
    }
}