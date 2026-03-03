using FluentValidation;
using SmartGreenhouseControlSystem.Application.ChangeThresholdCommand;

namespace SmartGreenhouseControlSystem.Application.Validators;

public class ChangeSoilHumidityValidator :AbstractValidator<ChangeSoilHumidityCommand>
{
    public ChangeSoilHumidityValidator()
    {
        RuleFor(x => x.DeviceId).NotNull().WithMessage("Device ID is required.");
        
        RuleFor(x => x.SoilHumidity)
            .NotNull().GreaterThanOrEqualTo(0).LessThanOrEqualTo(100)
            .WithMessage("Soil humidity must be between 0 and 100.");
    }
}