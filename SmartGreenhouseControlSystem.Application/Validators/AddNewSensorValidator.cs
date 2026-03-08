using FluentValidation;
using SmartGreenhouseControlSystem.Application.Commands.AddNewSensorCommand;

namespace SmartGreenhouseControlSystem.Application.Validators;

public class AddNewSensorValidator : AbstractValidator<AddNewSensorCommand>
{
    public   AddNewSensorValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.")
            .MaximumLength(50).WithMessage("Name must be less than 50 characters.");
        
        RuleFor(x => x.Type).IsInEnum().WithMessage("Sensor type must be one of: Digital, Analog.");
        
        RuleFor(x => x.Zone).GreaterThan(0).WithMessage("Zone must be positive.");
    }
}