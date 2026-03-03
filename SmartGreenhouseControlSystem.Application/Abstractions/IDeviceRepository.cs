using Domain;
using Domain.Components;

namespace SmartGreenhouseControlSystem.Application.Abstractions;

public interface IDeviceRepository
{
    Task AddDeviceAsync(Device device, CancellationToken cancellationToken = default);
    Task<Device?> FindDeviceAsync(Guid deviceId, CancellationToken cancellationToken = default);
    IQueryable<Device> GetAllDevices();
    
    Task AddTelemetryAsync(Telemetry telemetry, CancellationToken cancellationToken = default);
}