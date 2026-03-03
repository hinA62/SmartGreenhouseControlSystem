using FluentValidation;
using SmartGreenhouseControlSystem.Application.Commands.RegisterTelemetryDataCommand;

namespace SmartGreenhouseControlSystem.Application.Validators;

public class RegisterTelemetryDataCommandValidator : AbstractValidator<RegisterTelemetryDataCommand>
{
    public RegisterTelemetryDataCommandValidator()
    {
        RuleFor(x => x.DeviceId).NotNull();
        RuleFor(x => x.Timestamp).NotNull().GreaterThan(DateTime.Now.AddMinutes(-1));
        RuleFor(x => x.Temperature).NotNull().GreaterThan(-20).LessThan(35);
        RuleFor(x => x.AirHumidity).NotNull().GreaterThan(0).LessThan(100);
        RuleFor(x => x.SoilHumidity).NotNull().GreaterThan(0).LessThan(100);
    }
}