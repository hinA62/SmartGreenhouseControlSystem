using Domain.Enums;

namespace Domain.Components;

public class Sensor
{
    private Sensor() { }

    public static Sensor AddSensorToSystem(string name, SensorType type, int zone)
    {
        return new Sensor()
        {
            Name = name,
            Type = type,
            Zone = zone
        };
    }

    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid DeviceId { get; private set; } = Guid.Empty;
    public string Name { get; private set; } = string.Empty;
    public SensorType Type { get; private set; }
    public int Zone { get; private set; }
    
    public void SetDevice(Guid deviceId)
    {
        DeviceId = deviceId;
    }
}