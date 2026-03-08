using MediatR;

namespace SmartGreenhouseControlSystem.Application.Commands.ChangeThresholdCommand;

public record ChangeTemperatureCommand(Guid DeviceId, double Temperature) : IRequest<Unit>;