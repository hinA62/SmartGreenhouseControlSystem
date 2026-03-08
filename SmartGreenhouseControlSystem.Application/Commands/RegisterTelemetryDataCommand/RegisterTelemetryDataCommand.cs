using MediatR;

namespace SmartGreenhouseControlSystem.Application.Commands.RegisterTelemetryDataCommand;
    
public record RegisterTelemetryDataCommand
    (Guid DeviceId, DateTime Timestamp, 
        double Temperature, 
        double AirHumidity, 
        double SoilHumidity) 
    : IRequest<Guid>;