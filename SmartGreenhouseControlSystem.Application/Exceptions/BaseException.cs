namespace SmartGreenhouseControlSystem.Application.Exceptions;

public class BaseException(string name, string message) : Exception(message)
{ 
        public string ExceptionName { get; private set;} = name;
        public string ExceptionMessage { get; private set;} = message;
}