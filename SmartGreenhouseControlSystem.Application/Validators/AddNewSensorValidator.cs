using FluentValidation;
using SmartGreenhouseControlSystem.Application.Commands.AddNewSensorCommand;

namespace SmartGreenhouseControlSystem.Application.Validators;

public class AddNewSensorValidator : AbstractValidator<AddNewSensorCommand>
{
    public   AddNewSensorValidator()
    {
        RuleFor(x => x.Name).NotNull().MaximumLength(50)
            .WithMessage("Name required and must be less than 50 characters.");
        
        RuleFor(x => x.Type).NotNull().IsInEnum().WithMessage("Sensor type is required.");
        
        RuleFor(x => x.Zone).NotNull().WithMessage("Zone is required.");
    }
}