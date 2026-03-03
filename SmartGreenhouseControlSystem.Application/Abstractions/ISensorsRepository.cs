using Domain.Components;

namespace SmartGreenhouseControlSystem.Application.Abstractions;

public interface ISensorsRepository
{
    Task AddAsync(Sensor sensor, CancellationToken cancellationToken = default);
    Task<Sensor?> FindAsync(Guid sensorId, CancellationToken cancellationToken = default);
    
    IQueryable<Sensor> GetAllSensors();
}