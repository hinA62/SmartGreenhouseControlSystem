using Domain.Enums;
using MediatR;

namespace SmartGreenhouseControlSystem.Application.Commands.AddNewSensorCommand;

public record AddNewSensorCommand(string Name, SensorType Type, int Zone) : IRequest<Unit>;