using MediatR;

namespace SmartGreenhouseControlSystem.Application.ChangeThresholdCommand;

public record ChangeSoilHumidityCommand(Guid DeviceId, double SoilHumidity) : IRequest<Unit>;