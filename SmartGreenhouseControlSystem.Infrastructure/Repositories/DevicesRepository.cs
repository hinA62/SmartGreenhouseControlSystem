using Domain;
using Domain.Components;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using SmartGreenhouseControlSystem.Application.Abstractions;

namespace Infrastructure.Repositories;

public class DevicesRepository(SgcSystemDbContext context) : IDevicesRepository
{
    public async Task AddDeviceAsync(Device device, CancellationToken cancellationToken = default)
    {
        await context.Devices.AddAsync(device, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Device?> FindDeviceAsync(Guid deviceId, CancellationToken cancellationToken = default)
    {
        return await context.Devices.FindAsync([deviceId], cancellationToken);
    }

    public IQueryable<Device> GetAllDevices()
    {
        return context.Devices.AsQueryable();
    }

    public Task AddTelemetryAsync(Telemetry telemetry, CancellationToken cancellationToken = default)
    {
        context.Telemetries.Add(telemetry);
        return context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Telemetry?> GetLatestTelemetryAsync(Guid deviceId, CancellationToken cancellationToken)
    {
        var orderByDescending = context.Telemetries.Where(
            t => t.DeviceId == deviceId).OrderByDescending(t => t.Timestamp);
        
        return await orderByDescending.FirstOrDefaultAsync(cancellationToken);
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        return context.SaveChangesAsync(cancellationToken);
    }
}