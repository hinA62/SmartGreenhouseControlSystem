using SmartGreenhouseControlSystem.Application.Commands.ChangeThresholdCommand;
using SmartGreenhouseControlSystem.Application.Validators;

namespace SmartGreenhouseControlSystem.Test.ValidationTests;

public class ChangeTemperatureValidationTests
{
    [Fact]
    public void Given_Valid_Input_When_Validate_ShouldPass()
    {
        // Arrange
        var model = new ChangeTemperatureCommand(Guid.NewGuid(), 25.5);
        var validator = new ChangeTemperatureValidator();
        
        // Act
        var result = validator.Validate(model);
        
        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public void Given_EmptyName_When_Validate_ShouldFail()
    {
        // Arrange
        var model = new ChangeTemperatureCommand(Guid.Empty, 25.5);
        var validator = new ChangeTemperatureValidator();
        
        // Act
        var result = validator.Validate(model);
        
        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage == "Device ID is required.");  
    }

    [Theory]
    [InlineData(-31.17)]
    [InlineData(50)]
    public void Given_ExtremeTempertureValue_When_Validate_ShouldFail(double temperture)
    {
        // Arrange
        var model = new ChangeTemperatureCommand(Guid.NewGuid(), temperture);
        var validator = new ChangeTemperatureValidator();
        
        // Act
        var result = validator.Validate(model);
        
        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage == "Temperature must be between -20 and 35 degrees Celsius.");
    }
}