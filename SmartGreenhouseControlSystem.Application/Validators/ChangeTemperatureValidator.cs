using FluentValidation;
using SmartGreenhouseControlSystem.Application.ChangeThresholdCommand;

namespace SmartGreenhouseControlSystem.Application.Validators;

public class ChangeTemperatureValidator : AbstractValidator<ChangeTemperatureCommand>
{
    public ChangeTemperatureValidator()
    {
        RuleFor(x => x.DeviceId).NotNull().WithMessage("Device ID is required.");
        
        RuleFor(x => x.Temperature)
            .InclusiveBetween(-20, 35)
            .WithMessage("Temperature must be between -20 and 35 degrees Celsius.");
    }
}