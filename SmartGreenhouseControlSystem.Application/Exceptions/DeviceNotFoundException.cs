using System.Diagnostics.CodeAnalysis;

namespace SmartGreenhouseControlSystem.Application.Exceptions;

[ExcludeFromCodeCoverage]
public sealed class DeviceNotFoundException(Guid deviceId) 
    : BaseException("DeviceNotFoundException", $"Device with id {deviceId} not found");