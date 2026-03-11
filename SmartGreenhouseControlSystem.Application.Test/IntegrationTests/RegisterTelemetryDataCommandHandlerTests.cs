using Domain.Components;
using FluentAssertions;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmartGreenhouseControlSystem.Application.Abstractions;
using SmartGreenhouseControlSystem.Application.Commands.RegisterTelemetryDataCommand;
using SmartGreenhouseControlSystem.Application.Exceptions;

namespace SmartGreenhouseControlSystem.Test.IntegrationTests;

public class RegisterTelemetryDataCommandHandlerTests : IDisposable
{
    private readonly SgcSystemDbContext _context;
    private readonly ILogger<RegisterTelemetryDataCommandHandler> _logger;
    private readonly RegisterTelemetryDataCommandHandler _handler;
    private readonly IDevicesRepository _devicesRepository;

    public RegisterTelemetryDataCommandHandlerTests()
    {
        var options = new DbContextOptionsBuilder<SgcSystemDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _context = new SgcSystemDbContext(options);
        _devicesRepository = new DevicesRepository(_context);
        _logger = new LoggerFactory().CreateLogger<RegisterTelemetryDataCommandHandler>();
        _handler = new RegisterTelemetryDataCommandHandler(_devicesRepository, _logger);
    }
    
    [Fact]
    public async Task Given_ValidRegisterTelemetryDataCommand_When_Handle_ShouldRegisterTelemetryData()
    {
        // Arrange
        var systemId = new Guid("345d1d13-8c66-4e25-8394-df8d773cb339");
        var device = Device.AddDeviceToSystem("ESP32", systemId);
        _context.Devices.Add(device);
        await _context.SaveChangesAsync();
        
        var request = new RegisterTelemetryDataCommand(
            device.Id, 
            DateTime.Now, 
            25.0, 
            60.0, 
            40.0);
        
        // Act
        var response = await _handler.Handle(request, CancellationToken.None);
        
        // Assert
        response.Should().NotBeEmpty();
        
        var telemetry = await _devicesRepository.GetLatestTelemetryAsync(device.Id, CancellationToken.None);
        telemetry.Should().NotBeNull();
        telemetry.Temperature.Should().Be(25.0);
        telemetry.AirHumidity.Should().Be(60.0);
        telemetry.SoilHumidity.Should().Be(40.0);
        
        _logger.LogInformation("Telemetry data successfully registered.");
    }

    [Fact]
    public async Task Given_ValidationError_When_Handle_ShouldThrowValidationErrorException()
    {
        // Arrange
        var systemId = Guid.NewGuid();
        var device = Domain.Components.Device.AddDeviceToSystem("ESP32", systemId);
        _context.Devices.Add(device);
        await _context.SaveChangesAsync();
        
        var request = new RegisterTelemetryDataCommand(
            device.Id, 
            DateTime.UtcNow, 
            -100.0,
            60.0, 
            40.0);
        
        // Act
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);
        
        // Assert
        await act.Should().ThrowAsync<ValidationErrorException>();
        _logger.LogInformation("RegisterTelemetryDataCommand validation failed.");
    }
    
    [Fact]
    public async Task Given_NullOrNonExistingDevice_When_Handle_ShouldThrowDeviceNotFoundException()
    {
        // Arrange
        var nonExistingDeviceId = Guid.NewGuid();
        var request = new RegisterTelemetryDataCommand(nonExistingDeviceId, DateTime.Now, 25.0, 60.0, 40.0);
        
        // Act
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);
        
        // Assert
        await act.Should().ThrowAsync<DeviceNotFoundException>();
        _logger.LogInformation("Device not found.");
    }
    
    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
        
        _logger.LogInformation("Database disposed.");
        
        GC.SuppressFinalize(this);
    }
}