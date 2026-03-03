using MediatR;

namespace SmartGreenhouseControlSystem.Application.ChangeThresholdCommand;

public record ChangeTemperatureCommand(Guid DeviceId, double Temperature) : IRequest<Unit>;