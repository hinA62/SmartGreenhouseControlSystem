using System.Diagnostics.CodeAnalysis;

namespace SmartGreenhouseControlSystem.Application.Exceptions;

[ExcludeFromCodeCoverage]
public class ValidationErrorException() 
    : BaseException("ValidationErrorException", "A validation error has occurred. Please recheck input.");