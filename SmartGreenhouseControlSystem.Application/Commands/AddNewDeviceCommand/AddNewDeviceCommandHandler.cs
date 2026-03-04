using Domain.Components;
using MediatR;
using SmartGreenhouseControlSystem.Application.Abstractions;

namespace SmartGreenhouseControlSystem.Application.Commands.AddNewDeviceCommand;

public class AddNewDeviceCommandHandler(IDevicesRepository devicesRepository) 
    : IRequestHandler<AddNewDeviceCommand, Unit>
{
    public Task<Unit> Handle(AddNewDeviceCommand request, CancellationToken cancellationToken)
    {
        var device = Device.AddDeviceToSystem(request.Name, request.SystemId);
        devicesRepository.AddDeviceAsync(device, cancellationToken);
        return Task.FromResult(Unit.Value);
    }
}