using MediatR;

namespace SmartGreenhouseControlSystem.Application.ChangeThresholdCommand;

public record ChangeAirHumidityCommand(Guid DeviceId, double AirHumidity) : IRequest<Unit>;