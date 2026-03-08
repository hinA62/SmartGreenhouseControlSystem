using MediatR;
using Microsoft.Extensions.Logging;
using SmartGreenhouseControlSystem.Application.Abstractions;
using SmartGreenhouseControlSystem.Application.Exceptions;
using SmartGreenhouseControlSystem.Application.Validators;

namespace SmartGreenhouseControlSystem.Application.Commands.ChangeThresholdCommand;

public class ChangeSoilHumidityCommandHandler
    (IDevicesRepository devicesRepository, ILogger<ChangeSoilHumidityCommandHandler> logger) 
    : IRequestHandler<ChangeSoilHumidityCommand, Unit>
{
    public async Task<Unit> Handle(ChangeSoilHumidityCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("ChangeSoilHumidityCommand received.");

        var validator = new ChangeSoilHumidityValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            logger.LogError("ChangeSoilHumidityCommand validation failed.");
            throw new ValidationErrorException();
        }
        
        var device = await devicesRepository.FindDeviceAsync(request.DeviceId, cancellationToken);
        if (device is null)
        {
            logger.LogError("Couldn't change soil humidity. Device not found.");
            throw new DeviceNotFoundException(request.DeviceId);
        }

        device.ChangeSoilHumidityThreshold(request.SoilHumidity);
        await devicesRepository.SaveChangesAsync(cancellationToken);
        logger.LogInformation("Soil humidity successfully changed.");
        return await Task.FromResult(Unit.Value);
    }
}