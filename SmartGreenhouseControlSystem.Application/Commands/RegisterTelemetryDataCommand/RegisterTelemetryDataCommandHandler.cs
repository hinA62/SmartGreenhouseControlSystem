using Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using SmartGreenhouseControlSystem.Application.Abstractions;
using SmartGreenhouseControlSystem.Application.Exceptions;
using SmartGreenhouseControlSystem.Application.Validators;

namespace SmartGreenhouseControlSystem.Application.Commands.RegisterTelemetryDataCommand;

public class RegisterTelemetryDataCommandHandler
    (IDevicesRepository devicesRepository, ILogger<RegisterTelemetryDataCommandHandler> logger)
    : IRequestHandler<RegisterTelemetryDataCommand, Guid>
{
    public async Task<Guid> Handle(RegisterTelemetryDataCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("RegisterTelemetryDataCommand received.");

        var validator = new RegisterTelemetryDataCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            logger.LogInformation("RegisterTelemetryDataCommand validation failed.");
            throw new ValidationErrorException();
        }
        
        var device = await devicesRepository.FindDeviceAsync(request.DeviceId, cancellationToken);
        if (device is null)
        {
            logger.LogError("Couldn't register telemetry data. Device not found.");
            throw new DeviceNotFoundException(request.DeviceId);
        }

        var telemetry = Telemetry.CreateTelemetry(
            request.DeviceId,
            request.Temperature,
            request.AirHumidity,
            request.SoilHumidity);
        
        await devicesRepository.AddTelemetryAsync(telemetry, cancellationToken);
        logger.LogInformation("Telemetry data successfully registered.");
        return await Task.FromResult(telemetry.Id);
    }
}