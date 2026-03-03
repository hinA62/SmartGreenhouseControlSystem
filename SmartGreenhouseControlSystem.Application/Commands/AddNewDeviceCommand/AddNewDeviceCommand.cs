using MediatR;

namespace SmartGreenhouseControlSystem.Application.Commands.AddNewDeviceCommand;

public record AddNewDeviceCommand(string Name, Guid SystemId, List<Guid> SensorIds) : IRequest<Unit>;