using Domain;
using MediatR;
using SmartGreenhouseControlSystem.Application.Abstractions;

namespace SmartGreenhouseControlSystem.Application.Commands.RegisterTelemetryDataCommand;

public class RegisterTelemetryDataCommandHandler(IDeviceRepository deviceRepository)
    : IRequestHandler<RegisterTelemetryDataCommand, Guid>
{
    public Task<Guid> Handle(Commands.RegisterTelemetryDataCommand.RegisterTelemetryDataCommand request, CancellationToken cancellationToken)
    {
        var device = deviceRepository.FindDeviceAsync(request.DeviceId, cancellationToken);
        if (device is null)
        {
            throw new Exception("Device not found.");
        }

        var telemetry = Telemetry.CreateTelemetry(
            request.DeviceId,
            request.Temperature,
            request.AirHumidity,
            request.SoilHumidity);
        
        deviceRepository.AddTelemetryAsync(telemetry, cancellationToken);
        return Task.FromResult(telemetry.Id);
    }
}