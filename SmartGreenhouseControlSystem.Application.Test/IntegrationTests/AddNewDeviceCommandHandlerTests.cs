using FluentAssertions;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmartGreenhouseControlSystem.Application.Abstractions;
using SmartGreenhouseControlSystem.Application.Commands.AddNewDeviceCommand;
using SmartGreenhouseControlSystem.Application.Exceptions;

namespace SmartGreenhouseControlSystem.Test.IntegrationTests;

public class AddNewDeviceCommandHandlerTests : IDisposable
{
    private readonly SgcSystemDbContext _context;
    private readonly IDevicesRepository _devicesRepository;
    private readonly ILogger<AddNewDeviceCommandHandler> _logger;
    private readonly AddNewDeviceCommandHandler _handler;

    public AddNewDeviceCommandHandlerTests()
    {
        var options = new DbContextOptionsBuilder<SgcSystemDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new SgcSystemDbContext(options);
        _devicesRepository = new DevicesRepository(_context);
        _logger = new LoggerFactory().CreateLogger<AddNewDeviceCommandHandler>();
        _handler = new AddNewDeviceCommandHandler(_devicesRepository, _logger);
    }

    [Fact]
    public async Task Given_ValidAddNewDeviceCommand_When_Handle_ShouldAddDevice()
    {
        // Arrange
        var request = new AddNewDeviceCommand(
            "ESP32",
            Guid.NewGuid(),
            [Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid()]);
        
        // Act
        var response = await _handler.Handle(request, CancellationToken.None);
        
        // Assert
        response.Should().Be(Unit.Value);

        var devices = _devicesRepository.GetAllDevices();
        devices.AsEnumerable().Should().Contain(d => d.Name == "ESP32" && d.SystemId == request.SystemId);
        
        
        _logger.LogInformation("Device successfully added."); 
    }

    [Fact]
    public async Task Given_ValidationError_When_Handle_ShouldThrowValidationErrorException()
    {
        // Arrange
        var request = new AddNewDeviceCommand(
            string.Empty, 
            Guid.NewGuid(), 
            [Guid.NewGuid(), Guid.NewGuid()]);
        
        // Act
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);
        
        // Assert
        await act.Should().ThrowAsync<ValidationErrorException>();
        
        _logger.LogInformation("AddNewDeviceCommand validation failed.");
    }
    
    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
        
        _logger.LogInformation("Database disposed.");
        
        GC.SuppressFinalize(this);
    }
}