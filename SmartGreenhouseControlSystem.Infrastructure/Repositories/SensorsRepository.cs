using Domain.Components;
using Infrastructure.Persistence;
using SmartGreenhouseControlSystem.Application.Abstractions;

namespace Infrastructure.Repositories;

public class SensorsRepository(SgcSystemDbContext context) : ISensorsRepository
{
    private readonly SgcSystemDbContext _context = context;

    public async Task AddAsync(Sensor sensor, CancellationToken cancellationToken = default)
    {
        await _context.Sensors.AddAsync(sensor, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Sensor?> FindAsync(Guid sensorId, CancellationToken cancellationToken = default)
    {
        return await _context.Sensors.FindAsync([sensorId], cancellationToken);
    }

    public IQueryable<Sensor> GetAllSensors()
    {
        return _context.Sensors.AsQueryable();
    }
}