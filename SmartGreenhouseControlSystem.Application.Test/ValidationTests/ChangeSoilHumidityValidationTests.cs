using SmartGreenhouseControlSystem.Application.Commands.ChangeThresholdCommand;
using SmartGreenhouseControlSystem.Application.Validators;

namespace SmartGreenhouseControlSystem.Test.ValidationTests;

public class ChangeSoilHumidityValidationTests
{
    [Fact]
    public void Given_Valid_Input_When_Validate_ShouldPass()
    {
        // Arrange
        var model = new ChangeSoilHumidityCommand(Guid.NewGuid(), 50.0);
        var validator = new ChangeSoilHumidityValidator();
        
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
        var model = new ChangeSoilHumidityCommand(Guid.Empty, 84.2);
        var validator = new ChangeSoilHumidityValidator();
        
        // Act
        var result = validator.Validate(model);
        
        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage == "Device ID is required.");
    }
    
    [Theory]
    [InlineData (-1)]
    [InlineData (123)]
    public void Given_InvalidSoilHumidityValue_When_Validate_ShouldFail(double soilHumidity)
    {
        // Arrange
        var model = new ChangeSoilHumidityCommand(Guid.NewGuid(), soilHumidity);
        var validator = new ChangeSoilHumidityValidator();
        
        // Act
        var result = validator.Validate(model);
        
        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage == "Soil humidity must be between 0 and 100.");
    }
}