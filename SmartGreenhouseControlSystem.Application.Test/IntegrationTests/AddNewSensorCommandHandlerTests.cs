using Domain.Enums;
using FluentAssertions;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmartGreenhouseControlSystem.Application.Abstractions;
using SmartGreenhouseControlSystem.Application.Commands.AddNewSensorCommand;
using SmartGreenhouseControlSystem.Application.Exceptions;

namespace SmartGreenhouseControlSystem.Test.IntegrationTests;

public class AddNewSensorCommandHandlerTests : IDisposable
{
    private readonly SgcSystemDbContext _context;
    private readonly ISensorsRepository _sensorsRepository;
    private readonly AddNewSensorCommandHandler _handler;
    private readonly ILogger<AddNewSensorCommandHandler> _logger;

    public AddNewSensorCommandHandlerTests()
    {
        var options = new DbContextOptionsBuilder<SgcSystemDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _context = new SgcSystemDbContext(options);
        _sensorsRepository = new SensorsRepository(_context);
        _logger = new LoggerFactory().CreateLogger<AddNewSensorCommandHandler>();
        _handler = new AddNewSensorCommandHandler(_sensorsRepository, _logger);
    }

    [Fact]
    public async Task Given_ValidAddNewSensorCommand_When_Handle_ShouldAddSensor()
    {
        // Arrange
        var request = new AddNewSensorCommand(
            "Dht22",
            SensorType.Digital,
            4);
        
        // Act
        var response = await _handler.Handle(request, CancellationToken.None);
        
        // Assert
        response.Should().Be(Unit.Value);
        
        var sensors = _sensorsRepository.GetAllSensors();
        sensors.AsEnumerable().Should().Contain(s => s.Name == "Dht22" && s.Type == SensorType.Digital);
        
        _logger.LogInformation("Sensor successfully added.");
    }

    [Fact]
    public async Task Guven_ValidationError_When_Handle_ShouldThrowValidationErrorException()
    {
        // Arrange
        var request = new AddNewSensorCommand(
            string.Empty,
            SensorType.Digital,
            4);
        
        // Act
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);
        
        // Assert
        await act.Should().ThrowAsync<ValidationErrorException>();
        
        _logger.LogInformation("AddNewSensorCommand validation failed.");
    }
    
    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
        
        _logger.LogInformation("Database disposed.");
        
        GC.SuppressFinalize(this);
    }
}