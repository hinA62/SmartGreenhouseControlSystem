using SmartGreenhouseControlSystem.Application.Commands.ChangeThresholdCommand;
using SmartGreenhouseControlSystem.Application.Validators;

namespace SmartGreenhouseControlSystem.Test.ValidationTests;

public class ChangeAirHumidityValidationTests
{
    [Fact]
    public void Given_Valid_Input_When_Validate_ShouldPass()
    {
        // Arrange
        var model = new ChangeAirHumidityCommand(Guid.NewGuid(), 60.4);
        var validator = new ChangeAirHumidityValidator();
        
        // Act
        var result = validator.Validate(model);
        
        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors); 
    }
    
    [Fact]
    public void Given_Empty_DeviceId_When_Validate_ShouldFail()
    {
        // Arrange
        var model = new ChangeAirHumidityCommand(Guid.Empty, 34.5);
        var validator = new ChangeAirHumidityValidator();
        
        // Act
        var result = validator.Validate(model);
        
        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage == "Device ID is required.");
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(101)]
    public void Given_Invalid_AirHumidity_When_Validate_ShouldFail(double airHumidity)
    {
        // Arrange
        var model = new ChangeAirHumidityCommand(Guid.NewGuid(), airHumidity);
        var validator = new ChangeAirHumidityValidator();
        
        // Act
        var result = validator.Validate(model);
        
        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage == "Air humidity must be between 0 and 100.");
    }
}