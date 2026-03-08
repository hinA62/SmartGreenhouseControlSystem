using Domain.Enums;
using SmartGreenhouseControlSystem.Application.Commands.AddNewSensorCommand;
using SmartGreenhouseControlSystem.Application.Validators;

namespace SmartGreenhouseControlSystem.Test.ValidationTests;

public class AddNewSensorValidationTests
{
    [Fact]
    public void Given_Valid_Input_When_Validate_ShouldPass()
    {
        // Arrange
        var model = new AddNewSensorCommand("Dht22", SensorType.Digital, 3);
        var validator = new AddNewSensorValidator();
        
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
        var model = new AddNewSensorCommand(string.Empty, SensorType.Analog, 1);
        var validator = new AddNewSensorValidator();
        
        // Act
        var result = validator.Validate(model);
        
        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage == "Name is required.");
    }
    
    [Theory]
    [InlineData ("SensorWithAVeryLongNameThatExceedsFiftyCharacters*****")]
    public void Given_NameExceedsFiftyCharacters_When_Validate_ShouldFail(string name)
    {
        // Arrange
        var model = new AddNewSensorCommand(name, SensorType.Digital, 2);
        var validator = new AddNewSensorValidator();
        
        // Act
        var result = validator.Validate(model);
        
        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage == "Name must be less than 50 characters.");
    }

    [Fact]
    public void Given_InvalidSensorType_When_Validate_ShouldFail()
    {
        // Arrange
        var model = new AddNewSensorCommand("Dht22", (SensorType)999, 3);
        var validator = new AddNewSensorValidator();
        
        // Act
        var result = validator.Validate(model);
        
        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage == "Sensor type must be one of: Digital, Analog.");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Given_ZeroOrNegativeZone_When_Validate_ShouldFail(int p)
    {
        // Arrange
        var model = new AddNewSensorCommand("Dht22", SensorType.Digital, p);
        var validator = new AddNewSensorValidator();
        
        // Act
        var result = validator.Validate(model);
        
        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage == "Zone must be positive.");
    }
}