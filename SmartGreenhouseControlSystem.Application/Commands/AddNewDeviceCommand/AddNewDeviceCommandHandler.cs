using Domain.Components;
using MediatR;
using SmartGreenhouseControlSystem.Application.Abstractions;

namespace SmartGreenhouseControlSystem.Application.Commands.AddNewDeviceCommand;

public class AddNewDeviceCommandHandler(IDeviceRepository deviceRepository) 
    : IRequestHandler<AddNewDeviceCommand, Unit>
{
    public Task<Unit> Handle(Commands.AddNewDeviceCommand.AddNewDeviceCommand request, CancellationToken cancellationToken)
    {
        var device = Device.AddDeviceToSystem(request.Name, request.SystemId);
        deviceRepository.AddDeviceAsync(device, cancellationToken);
        return Task.FromResult(Unit.Value);
    }
}