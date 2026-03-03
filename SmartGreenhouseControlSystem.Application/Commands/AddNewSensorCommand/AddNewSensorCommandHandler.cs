using Domain.Components;
using MediatR;
using SmartGreenhouseControlSystem.Application.Abstractions;

namespace SmartGreenhouseControlSystem.Application.Commands.AddNewSensorCommand;

public class AddNewSensorCommandHandler(ISensorsRepository sensorsRepository)
    : IRequestHandler<AddNewSensorCommand, Unit>
{
    public Task<Unit> Handle(Commands.AddNewSensorCommand.AddNewSensorCommand request, CancellationToken cancellationToken)
    {
        var sensor = Sensor.AddSensorToSystem(request.Name, request.Type, request.Zone);
        sensorsRepository.AddAsync(sensor, cancellationToken);
        return Task.FromResult(Unit.Value);
    }
}