using FluentValidation;
using SmartGreenhouseControlSystem.Application.Commands.RegisterTelemetryDataCommand;

namespace SmartGreenhouseControlSystem.Application.Validators;

public class RegisterTelemetryDataCommandValidator : AbstractValidator<RegisterTelemetryDataCommand>
{
    public RegisterTelemetryDataCommandValidator()
    {
        RuleFor(x => x.DeviceId).NotNull().WithMessage("Device ID is required.");
        
        RuleFor(x => x.Timestamp).NotNull().GreaterThan(DateTime.Now.AddMinutes(-1))
            .WithMessage("Timestamp must be greater than 1 minute ago.");
        
        RuleFor(x => x.Temperature).NotNull().GreaterThan(-20).LessThan(35)
            .WithMessage("Temperature must be between -20 and 35 degrees Celsius.");
        
        RuleFor(x => x.AirHumidity).NotNull().GreaterThan(0).LessThan(100)
            .WithMessage("Air humidity must be between 0 and 100 percent.");
        
        RuleFor(x => x.SoilHumidity).NotNull().GreaterThan(0).LessThan(100)
            .WithMessage("Soil humidity must be between 0 and 100 percent.");
    }
}