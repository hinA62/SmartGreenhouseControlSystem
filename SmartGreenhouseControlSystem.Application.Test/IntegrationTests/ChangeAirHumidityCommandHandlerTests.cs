using AngleSharp.Css.Values;
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

public class ChangeAirHumidityCommandHandlerTests : IDisposable
{
    private readonly SgcSystemDbContext _context;
    private readonly IDevicesRepository _devicesRepository;
    private readonly ILogger<ChangeAirHumidityCommandHandler> _logger;
    private readonly ChangeAirHumidityCommandHandler _handler;

    public ChangeAirHumidityCommandHandlerTests()
    {
        var options = new DbContextOptionsBuilder<SgcSystemDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _context = new SgcSystemDbContext(options);
        _devicesRepository = new DevicesRepository(_context);
        _logger = new LoggerFactory().CreateLogger<ChangeAirHumidityCommandHandler>();
        _handler = new ChangeAirHumidityCommandHandler(_devicesRepository, _logger);
    }
    
    [Fact]
    public async Task Given_ValidChangeAirHumidityCommand_When_Handle_ShouldChangeAirHumidity()
    {
        // Arrange
        var systemId = Guid.NewGuid();
        var device = Domain.Components.Device.AddDeviceToSystem("ESP32", systemId);
        _context.Devices.Add(device);
        await _context.SaveChangesAsync();
        
        var request = new ChangeAirHumidityCommand(device.Id, 60.0);
        
        // Act
        var response = await _handler.Handle(request, CancellationToken.None);
        
        // Assert
        response.Should().Be(Unit.Value);
        
        var updatedDevice = await _devicesRepository.FindDeviceAsync(device.Id);
        updatedDevice.Should().NotBeNull();
        updatedDevice.TargetAirHumidity.Should().Be(60.0);
        
        _logger.LogInformation("Air humidity threshold successfully changed."); 
    }

    [Fact]
    public async Task Given_ValidationError_When_Handle_ShouldThrowValidationErrorException()
    {
        // Arrange
        var systemId = Guid.NewGuid();
        var device = Domain.Components.Device.AddDeviceToSystem("ESP32", systemId);
        _context.Devices.Add(device);
        await _context.SaveChangesAsync();

        var request = new ChangeAirHumidityCommand(device.Id, -15);
        
        // Act
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);
        
        // Assert
        await act.Should().ThrowAsync<ValidationErrorException>();
        _logger.LogInformation("ChangeAirHumidityCommand validation failed.");
    }
    
    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
        
        _logger.LogInformation("Database disposed.");
        
        GC.SuppressFinalize(this);
    }
}