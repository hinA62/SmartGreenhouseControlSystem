using MediatR;
using SmartGreenhouseControlSystem.Application.Abstractions;
using SmartGreenhouseControlSystem.Application.Exceptions;

namespace SmartGreenhouseControlSystem.Application.ChangeThresholdCommand;

public class ChangeAirHumidityCommandHandler(IDevicesRepository devicesRepository) :
    IRequestHandler<ChangeAirHumidityCommand, Unit>
{
    public async Task<Unit> Handle(ChangeAirHumidityCommand request, CancellationToken cancellationToken)
    {
        var device = await devicesRepository.FindDeviceAsync(request.DeviceId, cancellationToken);
        if (device is null)
        {
            throw new DeviceNotFoundException(request.DeviceId);
        }

        device.ChangeAirHumidityThreshold(request.AirHumidity);
        await devicesRepository.SaveChangesAsync(cancellationToken);
        return await Task.FromResult(Unit.Value);
    }
}