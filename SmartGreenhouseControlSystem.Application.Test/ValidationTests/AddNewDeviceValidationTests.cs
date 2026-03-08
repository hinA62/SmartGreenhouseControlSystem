using SmartGreenhouseControlSystem.Application.Commands.AddNewDeviceCommand;
using SmartGreenhouseControlSystem.Application.Validators;

namespace SmartGreenhouseControlSystem.Test.ValidationTests;

public class AddNewDeviceValidationTests
{
    [Fact]
    public void Given_Valid_Input_When_Validate_ShouldPass()
    {
        // Arrange
        var model = new AddNewDeviceCommand("ESP32", Guid.NewGuid(), [Guid.NewGuid(), Guid.NewGuid()]);
        var validator = new AddNewDeviceValidator();

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
        var model = new AddNewDeviceCommand(string.Empty,Guid.NewGuid(), [Guid.NewGuid(), Guid.NewGuid()]);
        var validator = new AddNewDeviceValidator();
        
        // Act
        var result = validator.Validate(model);
        
        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage == "Name is required.");
    }

    [Theory]
    [InlineData("ESP32DeviceWithAVeryLongNameThatExceedsFiftyCharacters")]
    public void Given_NameWithMoreThanFiftyCharacters_When_Validate_ShouldFail(string name)
    {
        // Arrange
        var model = new AddNewDeviceCommand(name, Guid.NewGuid(), [Guid.NewGuid(), Guid.NewGuid()]);
        var validator = new AddNewDeviceValidator();
        
        // Act
        var result = validator.Validate(model);
        
        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage == "Name must be less than 50 characters.");
    }

    [Fact]
    public void Given_EmptySystemId_When_Validate_ShouldFail()
    {
        // Arrange
        var model = new AddNewDeviceCommand("ESP32", Guid.Empty, [Guid.NewGuid()]);
        var validator = new AddNewDeviceValidator();
        
        // Act
        var result = validator.Validate(model);
        
        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage == "System ID is required.");   
    }

    [Fact]
    public void Given_NullSensorIsdList_When_Validate_ShouldFail()
    {
        // Arrange
        var model = new AddNewDeviceCommand("ESP32", Guid.NewGuid(), null!);
        var validator = new AddNewDeviceValidator();
        
        // Act
        var result = validator.Validate(model);
        
        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage == "Sensor IDs are required.");  
    }
}