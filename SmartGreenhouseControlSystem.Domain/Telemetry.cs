namespace Domain;

public class Telemetry
{
    private Telemetry() { }

    public static Telemetry CreateTelemetry(Guid deviceId, double temperature, double airHumidity, double soilHumidity)
    {
        return new Telemetry()
        {
            DeviceId = deviceId,
            Timestamp = DateTime.Now,
            
            Temperature = temperature,
            AirHumidity = airHumidity,
            SoilHumidity = soilHumidity
        };
    }
    
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid DeviceId { get; private set; } = Guid.Empty;
    public DateTime Timestamp { get; private set; } = DateTime.Now;
    
    public double Temperature { get; private set; } = 0;
    public double AirHumidity { get; private set; } = 0;
    public double SoilHumidity { get; private set; } = 0;
    
    public void UpdateTelemetry(double temperature, double airHumidity, double soilHumidity)
    {
        Timestamp = DateTime.Now;
        
        Temperature = temperature;
        AirHumidity = airHumidity;
        SoilHumidity = soilHumidity;
    }
    
    
}