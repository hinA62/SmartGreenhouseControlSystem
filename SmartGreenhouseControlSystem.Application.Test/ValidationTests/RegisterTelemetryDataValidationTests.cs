using SmartGreenhouseControlSystem.Application.Commands.RegisterTelemetryDataCommand;
using SmartGreenhouseControlSystem.Application.Validators;

namespace SmartGreenhouseControlSystem.Test.ValidationTests;

public class RegisterTelemetryDataValidationTests
{
    [Fact]
    public void Given_Valid_Input_When_Validate_ShouldPass()
    {
        // Arrange
        var model = new RegisterTelemetryDataCommand(Guid.NewGuid(), DateTime.Now, 25.5, 50.0, 60.4);
        var validator = new RegisterTelemetryDataCommandValidator();
        
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
        var model = new RegisterTelemetryDataCommand(Guid.Empty, DateTime.Now, 25.5, 50.0, 60.4);
        var validator = new RegisterTelemetryDataCommandValidator();
        
        // Act
        var result = validator.Validate(model);
        
        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage == "Device ID is required."); 
    }
    
    [Theory]
    [InlineData ("2015-01-01T00:00:00")]
    public void Given_PastTimestamp_When_Validate_ShouldFail(DateTime timestamp)
    {
        // Arrange
        var model = new RegisterTelemetryDataCommand(Guid.NewGuid(), timestamp, 25.5, 50.0, 60.4);
        var validator = new RegisterTelemetryDataCommandValidator();
        
        // Act
        var result = validator.Validate(model);
        
        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage == "Timestamp must be greater than 1 minute ago."); 
    }

    [Theory]
    [InlineData(-100)]
    [InlineData(70)]
    public void Given_ExtremeTemperture_When_Validate_ShouldFail(double temperture)
    {
        // Arrange
        var model = new RegisterTelemetryDataCommand(Guid.NewGuid(), DateTime.Now, temperture, 50.0, 60.4);
        var validator = new RegisterTelemetryDataCommandValidator();
        
        // Act
        var result = validator.Validate(model);
        
        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage == "Temperature must be between -20 and 35 degrees Celsius.");
    }
    
    [Theory]
    [InlineData (-1)]
    [InlineData (342)]
    public void Given_InvalidAirHumidityValue_When_Validate_ShouldFail(double airHumidity)
    {
        // Arrange
        var model = new RegisterTelemetryDataCommand(Guid.NewGuid(), DateTime.Now, 25.5, airHumidity, 60.4);
        var validator = new RegisterTelemetryDataCommandValidator();
        
        // Act
        var result = validator.Validate(model);
        
        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage == "Air humidity must be between 0 and 100 percent.");
    }
    
    [Theory]
    [InlineData (-12)]
    [InlineData (421)]
    public void Given_InvalidSoilHumidityValue_When_Validate_ShouldFail(double soilHumidity)
    {
        // Arrange
        var model = new RegisterTelemetryDataCommand(Guid.NewGuid(), DateTime.Now, 25.5, 50.0, soilHumidity);
        var validator = new RegisterTelemetryDataCommandValidator();
        
        // Act
        var result = validator.Validate(model);
        
        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage == "Soil humidity must be between 0 and 100 percent.");
    }
}