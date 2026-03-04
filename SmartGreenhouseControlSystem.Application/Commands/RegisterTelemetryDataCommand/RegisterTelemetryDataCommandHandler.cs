using Domain;
using MediatR;
using SmartGreenhouseControlSystem.Application.Abstractions;
using SmartGreenhouseControlSystem.Application.Exceptions;

namespace SmartGreenhouseControlSystem.Application.Commands.RegisterTelemetryDataCommand;

public class RegisterTelemetryDataCommandHandler(IDevicesRepository devicesRepository)
    : IRequestHandler<RegisterTelemetryDataCommand, Guid>
{
    public Task<Guid> Handle(Commands.RegisterTelemetryDataCommand.RegisterTelemetryDataCommand request, CancellationToken cancellationToken)
    {
        var device = devicesRepository.FindDeviceAsync(request.DeviceId, cancellationToken);
        if (device is null)
        {
            throw new DeviceNotFoundException(request.DeviceId);
        }

        var telemetry = Telemetry.CreateTelemetry(
            request.DeviceId,
            request.Temperature,
            request.AirHumidity,
            request.SoilHumidity);
        
        devicesRepository.AddTelemetryAsync(telemetry, cancellationToken);
        return Task.FromResult(telemetry.Id);
    }
}