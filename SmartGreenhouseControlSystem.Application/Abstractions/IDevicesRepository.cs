using Domain;
using Domain.Components;

namespace SmartGreenhouseControlSystem.Application.Abstractions;

public interface IDevicesRepository
{
    Task AddDeviceAsync(Device device, CancellationToken cancellationToken = default);
    Task<Device?> FindDeviceAsync(Guid deviceId, CancellationToken cancellationToken = default);
    IQueryable<Device> GetAllDevices();
    
    Task AddTelemetryAsync(Telemetry telemetry, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken);
    Task<Telemetry?> GetLatestTelemetryAsync(Guid deviceId, CancellationToken none);
}