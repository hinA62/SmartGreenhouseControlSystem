namespace SmartGreenhouseControlSystem.Application.Exceptions;

public sealed class DeviceNotFoundException(Guid deviceId) 
    : BaseException("DeviceNotFoundException", $"Device with id {deviceId} not found");