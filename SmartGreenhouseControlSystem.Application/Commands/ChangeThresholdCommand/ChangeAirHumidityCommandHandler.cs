using MediatR;
using Microsoft.Extensions.Logging;
using SmartGreenhouseControlSystem.Application.Abstractions;
using SmartGreenhouseControlSystem.Application.Exceptions;
using SmartGreenhouseControlSystem.Application.Validators;

namespace SmartGreenhouseControlSystem.Application.Commands.ChangeThresholdCommand;

public class ChangeAirHumidityCommandHandler
    (IDevicesRepository devicesRepository, ILogger<ChangeAirHumidityCommandHandler> logger) 
    : IRequestHandler<ChangeAirHumidityCommand, Unit>
{
    public async Task<Unit> Handle(ChangeAirHumidityCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("ChangeAirHumidityCommand received.");
        
        var validator = new ChangeAirHumidityValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            logger.LogError("ChangeAirHumidityCommand validation failed.");
            throw new ValidationErrorException();
        }
        
        var device = await devicesRepository.FindDeviceAsync(request.DeviceId, cancellationToken);
        if (device is null)
        {
            logger.LogError("Couldn't change air humidity. Device not found.");
            throw new DeviceNotFoundException(request.DeviceId);
        }

        device.ChangeAirHumidityThreshold(request.AirHumidity);
        await devicesRepository.SaveChangesAsync(cancellationToken);
        logger.LogInformation("Air humidity successfully changed.");
        return await Task.FromResult(Unit.Value);
    }
}