namespace SmartGreenhouseControlSystem.Application.Exceptions;

public sealed class DeviceNotFoundException(Guid deviceId) 
    : Exception($"Device with id {deviceId} not found.");