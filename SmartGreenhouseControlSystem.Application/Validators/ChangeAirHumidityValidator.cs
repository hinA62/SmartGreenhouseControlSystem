using FluentValidation;
using SmartGreenhouseControlSystem.Application.ChangeThresholdCommand;

namespace SmartGreenhouseControlSystem.Application.Validators;

public class ChangeAirHumidityValidator : AbstractValidator<ChangeAirHumidityCommand>
{
    public ChangeAirHumidityValidator()
    {
        RuleFor(x => x.DeviceId).NotNull().WithMessage("Device ID is required.");
        
        RuleFor(x => x.AirHumidity)
            .NotNull().GreaterThanOrEqualTo(0).LessThanOrEqualTo(100)
            .WithMessage("Air humidity must be between 0 and 100.");
    }
}