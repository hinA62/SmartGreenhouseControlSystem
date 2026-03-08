using MediatR;

namespace SmartGreenhouseControlSystem.Application.Commands.ChangeThresholdCommand;

public record ChangeAirHumidityCommand(Guid DeviceId, double AirHumidity) : IRequest<Unit>;