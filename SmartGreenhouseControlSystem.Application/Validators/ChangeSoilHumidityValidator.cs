using FluentValidation;
using SmartGreenhouseControlSystem.Application.Commands.ChangeThresholdCommand;

namespace SmartGreenhouseControlSystem.Application.Validators;

public class ChangeSoilHumidityValidator :AbstractValidator<ChangeSoilHumidityCommand>
{
    public ChangeSoilHumidityValidator()
    {
        RuleFor(x => x.DeviceId).NotEmpty().WithMessage("Device ID is required.");
        
        RuleFor(x => x.SoilHumidity)
            .InclusiveBetween(0, 100)
            .WithMessage("Soil humidity must be between 0 and 100.");
    }
}