using MediatR;

namespace SmartGreenhouseControlSystem.Application.Commands.ChangeThresholdCommand;

public record ChangeSoilHumidityCommand(Guid DeviceId, double SoilHumidity) : IRequest<Unit>;