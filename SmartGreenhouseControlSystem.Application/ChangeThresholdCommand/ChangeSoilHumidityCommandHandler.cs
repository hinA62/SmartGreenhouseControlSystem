using MediatR;
using SmartGreenhouseControlSystem.Application.Abstractions;

namespace SmartGreenhouseControlSystem.Application.ChangeThresholdCommand;

public class ChangeSoilHumidityCommandHandler(IDevicesRepository devicesRepository) :
    IRequestHandler<ChangeSoilHumidityCommand, Unit>
{
    public async Task<Unit> Handle(ChangeSoilHumidityCommand request, CancellationToken cancellationToken)
    {
        var device = await devicesRepository.FindDeviceAsync(request.DeviceId, cancellationToken);
        if (device is null)
        {
            throw new Exception("Device not found");
        }

        device.ChangeSoilHumidityThreshold(request.SoilHumidity);
        await devicesRepository.SaveChangesAsync(cancellationToken);
        return await Task.FromResult(Unit.Value);
    }
}