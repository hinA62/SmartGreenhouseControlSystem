using MediatR;
using SmartGreenhouseControlSystem.Application.Abstractions;

namespace SmartGreenhouseControlSystem.Application.ChangeThresholdCommand;

public class ChangeTemperatureCommandHandler(IDevicesRepository devicesRepository)
    : IRequestHandler<ChangeTemperatureCommand, Unit>
{
    public async Task<Unit> Handle(ChangeTemperatureCommand request, CancellationToken cancellationToken)
    {
        var device = await devicesRepository.FindDeviceAsync(request.DeviceId, cancellationToken);
        if (device is null)
        {
            throw new Exception("Device not found");
        }

        device.ChangeTemperatureThreshold(request.Temperature);
        await devicesRepository.SaveChangesAsync(cancellationToken);
        return await Task.FromResult(Unit.Value);
    }
}