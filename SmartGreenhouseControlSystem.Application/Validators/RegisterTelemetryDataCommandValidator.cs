using FluentValidation;
using SmartGreenhouseControlSystem.Application.Commands.RegisterTelemetryDataCommand;

namespace SmartGreenhouseControlSystem.Application.Validators;

public class RegisterTelemetryDataCommandValidator : AbstractValidator<RegisterTelemetryDataCommand>
{
    public RegisterTelemetryDataCommandValidator()
    {
        RuleFor(x => x.DeviceId).NotEmpty().WithMessage("Device ID is required.");
        
        RuleFor(x => x.Timestamp).GreaterThan(DateTime.Now.AddMinutes(-1))
            .WithMessage("Timestamp must be greater than 1 minute ago.");
        
        RuleFor(x => x.Temperature).InclusiveBetween(-20, 35)
            .WithMessage("Temperature must be between -20 and 35 degrees Celsius.");
        
        RuleFor(x => x.AirHumidity).InclusiveBetween(0, 100)
            .WithMessage("Air humidity must be between 0 and 100 percent.");
        
        RuleFor(x => x.SoilHumidity).InclusiveBetween(0, 100)
            .WithMessage("Soil humidity must be between 0 and 100 percent.");
    }
}