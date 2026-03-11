using Domain.Components;
using FluentAssertions;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmartGreenhouseControlSystem.Application.Abstractions;
using SmartGreenhouseControlSystem.Application.Commands.ChangeThresholdCommand;
using SmartGreenhouseControlSystem.Application.Exceptions;

namespace SmartGreenhouseControlSystem.Test.IntegrationTests;

public class ChangeTemperatureCommandHandlerTests : IDisposable
{
    private readonly SgcSystemDbContext _context;  
    private readonly IDevicesRepository _devicesRepository;
    private readonly ILogger<ChangeTemperatureCommandHandler> _logger;
    private readonly ChangeTemperatureCommandHandler _handler;
    
    public ChangeTemperatureCommandHandlerTests()
    {
        var options = new DbContextOptionsBuilder<SgcSystemDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        
        _context = new SgcSystemDbContext(options);
        _devicesRepository = new DevicesRepository(_context);
        _logger = new LoggerFactory().CreateLogger<ChangeTemperatureCommandHandler>();
        _handler = new ChangeTemperatureCommandHandler(_devicesRepository, _logger);
    }

    [Fact]
    public async Task Given_ValidChangeTemperatureCommand_When_Handle_ShouldChangeTemperature()
    {
        // Arrange
        var systemId = Guid.NewGuid();
        var device = Domain.Components.Device.AddDeviceToSystem("ESP32", systemId);
        _context.Devices.Add(device);
        await _context.SaveChangesAsync();
        
        var request = new ChangeTemperatureCommand(device.Id, 25.0);
        
        // Act
        var response = await _handler.Handle(request, CancellationToken.None);
        
        // Assert
        response.Should().Be(Unit.Value);
        
        var updatedDevice = await _devicesRepository.FindDeviceAsync(device.Id);
        updatedDevice.Should().NotBeNull();
        updatedDevice.TargetTemperature.Should().Be(25.0);
        
        _logger.LogInformation("Temperature threshold successfully changed.");
    }

    [Fact]
    public async Task Given_ValidationError_When_Handle_ShouldThrowValidationErrorException()
    {
        // Arrange
        var systemId = new Guid("345d1d13-8c66-4e25-8394-df8d773cb339");
        var device = Device.AddDeviceToSystem("ESP32", systemId);
        _context.Devices.Add(device);
        await _context.SaveChangesAsync();
        
        var request = new ChangeTemperatureCommand(device.Id, 70.3);
        
        // Act
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);
        
        // Asset
        await act.Should().ThrowAsync<ValidationErrorException>();
        _logger.LogInformation("ChangeTemperatureCommand validation failed.");
    }

    [Fact]
    public async Task Given_NullOrNonexistingDevice_When_Handle_ShouldThrowDeviceNotFoundException()
    {
        // Arrange
        var nonExistingDeviceId = Guid.NewGuid();
        var request = new ChangeTemperatureCommand(nonExistingDeviceId, 25.0);
        
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