using MediatR;
using Microsoft.Extensions.Logging;
using SmartGreenhouseControlSystem.Application.Abstractions;
using SmartGreenhouseControlSystem.Application.Exceptions;
using SmartGreenhouseControlSystem.Application.Validators;

namespace SmartGreenhouseControlSystem.Application.Commands.ChangeThresholdCommand;

public class ChangeTemperatureCommandHandler
    (IDevicesRepository devicesRepository, ILogger<ChangeTemperatureCommandHandler> logger)
    : IRequestHandler<ChangeTemperatureCommand, Unit>
{
    public async Task<Unit> Handle(ChangeTemperatureCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("ChangeTemperatureCommand received.");

        var validator = new ChangeTemperatureValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            logger.LogError("ChangeTemperatureCommand validation failed.");
            throw new ValidationErrorException();
        }
        
        var device = await devicesRepository.FindDeviceAsync(request.DeviceId, cancellationToken);
        if (device is null)
        {
            logger.LogError("Couldn't change temperature. Device not found.");
            throw new DeviceNotFoundException(request.DeviceId);
        }

        device.ChangeTemperatureThreshold(request.Temperature);
        await devicesRepository.SaveChangesAsync(cancellationToken);
        logger.LogInformation("Temperature successfully changed.");
        return await Task.FromResult(Unit.Value);
    }
}