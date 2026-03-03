namespace Domain.Components;

public class Device
{
    private Device() { }

    public  static Device AddDeviceToSystem(string name, Guid systemId)
    {
        return new Device()
        {
            Name = name,
            SystemId = systemId,
            Sensors = []
        };
    }
    
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; } = string.Empty;
    public Guid SystemId { get; private set; } = Guid.Empty;
    public List<Sensor> Sensors { get; private set; } = [];
    
    
    public void BindSensorToDevice(Sensor sensor)
    {
        sensor.SetDevice(Id);
        Sensors.Add(sensor);
    }
    
    public void UnbindSensorFromDevice(Sensor sensor)
    {
        Sensors.Remove(sensor);
    }
}