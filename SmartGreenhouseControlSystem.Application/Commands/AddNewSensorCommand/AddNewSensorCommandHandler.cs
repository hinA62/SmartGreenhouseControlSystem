using Domain.Components;
using MediatR;
using Microsoft.Extensions.Logging;
using SmartGreenhouseControlSystem.Application.Abstractions;

namespace SmartGreenhouseControlSystem.Application.Commands.AddNewSensorCommand;

public class AddNewSensorCommandHandler
    (ISensorsRepository sensorsRepository, ILogger<AddNewSensorCommandHandler> logger)
    : IRequestHandler<AddNewSensorCommand, Unit>
{
    public async Task<Unit> Handle(AddNewSensorCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("AddNewSensorCommand received.");
        
        var validator = new Validators.AddNewSensorValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            logger.LogInformation("AddNewSensorCommand validation failed.");
            throw new Exceptions.ValidationErrorException();
        }
        
        var sensor = Sensor.AddSensorToSystem(request.Name, request.Type, request.Zone);
        await sensorsRepository.AddAsync(sensor, cancellationToken);
        logger.LogInformation("Sensor successfully added.");
        return await Task.FromResult(Unit.Value);
    }
}