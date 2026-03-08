using Domain.Components;
using MediatR;
using Microsoft.Extensions.Logging;
using SmartGreenhouseControlSystem.Application.Abstractions;
using SmartGreenhouseControlSystem.Application.Exceptions;
using SmartGreenhouseControlSystem.Application.Validators;

namespace SmartGreenhouseControlSystem.Application.Commands.AddNewDeviceCommand;

public class AddNewDeviceCommandHandler
    (IDevicesRepository devicesRepository, ILogger<AddNewDeviceCommandHandler> logger) 
    : IRequestHandler<AddNewDeviceCommand, Unit>
{
    public async Task<Unit> Handle(AddNewDeviceCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("AddNewDeviceCommand received.");
        var validator = new AddNewDeviceValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            logger.LogInformation("AddNewDeviceCommand validation failed.");
            throw new ValidationErrorException();
        }
        
        var device = Device.AddDeviceToSystem(request.Name, request.SystemId);
        await devicesRepository.AddDeviceAsync(device, cancellationToken);
        logger.LogInformation("Device successfully added.");
        return await Task.FromResult(Unit.Value);
    }
}