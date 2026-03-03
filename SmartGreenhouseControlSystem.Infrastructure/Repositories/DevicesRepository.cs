using Domain;
using Domain.Components;
using Infrastructure.Persistence;
using SmartGreenhouseControlSystem.Application.Abstractions;

namespace Infrastructure.Repositories;

public class DevicesRepository(SGCSystemDbContext context) : IDeviceRepository
{
    private readonly SGCSystemDbContext _context = context;

    
    public async Task AddDeviceAsync(Device device, CancellationToken cancellationToken = default)
    {
        await _context.Devices.AddAsync(device, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Device?> FindDeviceAsync(Guid deviceId, CancellationToken cancellationToken = default)
    {
        return await _context.Devices.FindAsync([deviceId], cancellationToken);
    }

    public IQueryable<Device> GetAllDevices()
    {
        return _context.Devices.AsQueryable();
    }

    public Task AddTelemetryAsync(Telemetry telemetry, CancellationToken cancellationToken = default)
    {
        _context.Telemetries.Add(telemetry);
        return _context.SaveChangesAsync(cancellationToken);
    }
}