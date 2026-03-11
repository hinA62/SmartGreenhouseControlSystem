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

public class ChangeSoilHumidityCommandHandlerTests : IDisposable
{
    private readonly SgcSystemDbContext _context;   
    private readonly ILogger<ChangeSoilHumidityCommandHandler> _logger;
    private readonly ChangeSoilHumidityCommandHandler _handler;
    private readonly IDevicesRepository _devicesRepository;

    public ChangeSoilHumidityCommandHandlerTests()
    {
        var options = new DbContextOptionsBuilder<SgcSystemDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _context = new SgcSystemDbContext(options);
        _devicesRepository = new DevicesRepository(_context);
        _logger = new LoggerFactory().CreateLogger<ChangeSoilHumidityCommandHandler>();
        _handler = new ChangeSoilHumidityCommandHandler(_devicesRepository, _logger);
    }
    
    [Fact]
    public async Task Given_ValidChangeSoilHumidityCommand_When_Handle_ShouldChangeSoilHumidity()
    {
        // Arrange
        var systemId = new Guid("345d1d13-8c66-4e25-8394-df8d773cb339");
        var device = Device.AddDeviceToSystem("ESP32", systemId);
        _context.Devices.Add(device);
        await _context.SaveChangesAsync();
        
        var request = new ChangeSoilHumidityCommand(device.Id, 40.0);
        
        // Act
        var response = await _handler.Handle(request, CancellationToken.None);
        
        // Assert
        response.Should().Be(Unit.Value);
        
        var updatedDevice = await _devicesRepository.FindDeviceAsync(device.Id);
        updatedDevice.Should().NotBeNull();
        updatedDevice.TargetSoilHumidity.Should().Be(40.0);
        
        _logger.LogInformation("Soil humidity threshold successfully changed."); 
    }

    [Fact]
    public async Task Given_ValidationError_When_Handle_ShouldThrowValidationErrorException()
    {
        // Arrange
        var systemId = new Guid("345d1d13-8c66-4e25-8394-df8d773cb339");
        var device = Device.AddDeviceToSystem("ESP32", systemId);
        _context.Devices.Add(device);
        await _context.SaveChangesAsync();
        
        var request = new ChangeSoilHumidityCommand(device.Id, -15);
        
        // Act
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);
        
        // Assert
        await act.Should().ThrowAsync<ValidationErrorException>();
        _logger.LogInformation("ChangeSoilHumidityCommand validation failed.");
    }

    [Fact]
    public async Task Given_NullOrNonExistingDevice_When_Handle_ShouldThrowDeviceNotFoundException()
    {
        // Arrange
        var nonExistingDeviceId = Guid.NewGuid();
        var request = new ChangeSoilHumidityCommand(nonExistingDeviceId, 40.0);
        
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