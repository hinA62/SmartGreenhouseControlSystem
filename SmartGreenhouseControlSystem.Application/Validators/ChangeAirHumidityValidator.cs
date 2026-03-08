using FluentValidation;
using SmartGreenhouseControlSystem.Application.Commands.ChangeThresholdCommand;

namespace SmartGreenhouseControlSystem.Application.Validators;

public class ChangeAirHumidityValidator : AbstractValidator<ChangeAirHumidityCommand>
{
    public ChangeAirHumidityValidator()
    {
        RuleFor(x => x.DeviceId).NotEmpty().WithMessage("Device ID is required.");
        
        RuleFor(x => x.AirHumidity)
            .InclusiveBetween(0, 100)
            .WithMessage("Air humidity must be between 0 and 100.");
    }
}