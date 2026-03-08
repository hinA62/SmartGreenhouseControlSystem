namespace Domain.Components;

public class Device
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; } = string.Empty;
    public Guid SystemId { get; private set; } = Guid.Empty;
    public List<Sensor> Sensors { get; private set; } = [];
    
    public double TargetTemperature { get; private set; }
    public double CurrentTemperature { get; private set; }
    public double TargetAirHumidity { get; private set; }  
    public double CurrentAirHumidity { get; private set; }
    public double TargetSoilHumidity { get; private set; }
    public double CurrentSoilHumidity { get; private set; }

    private Device() { }

    public  static Device AddDeviceToSystem(string name, Guid systemId)
    {
        return new Device()
        {
            Name = name,
            SystemId = systemId,
        };
    }
    public void BindSensorToDevice(Sensor sensor)
    {
        sensor.SetDevice(Id);
        Sensors.Add(sensor);
    }
    
    public void UnbindSensorFromDevice(Sensor sensor)
    {
        Sensors.Remove(sensor);
    }

    public void ChangeTemperatureThreshold(double requestTemperature)
    {
        TargetTemperature = requestTemperature;
    }
    
    public void ChangeAirHumidityThreshold(double requestAirHumidity)
    {
        TargetAirHumidity = requestAirHumidity;
    }
    
    public void ChangeSoilHumidityThreshold(double requestSoilHumidity)
    {
        TargetSoilHumidity = requestSoilHumidity;
    }
    
    
}