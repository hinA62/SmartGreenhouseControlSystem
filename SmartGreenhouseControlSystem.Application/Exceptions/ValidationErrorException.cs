namespace SmartGreenhouseControlSystem.Application.Exceptions;

public class ValidationErrorException() 
    : BaseException("ValidationErrorException", "A validation error has occurred. Please recheck input.");