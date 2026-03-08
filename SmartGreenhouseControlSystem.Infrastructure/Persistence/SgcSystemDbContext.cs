using Domain;
using Domain.Components;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class SgcSystemDbContext (DbContextOptions<SgcSystemDbContext> options) : DbContext(options)
{
    public DbSet<Device> Devices => Set<Device>();
    public DbSet<Sensor> Sensors => Set<Sensor>();
    public DbSet<Telemetry> Telemetries => Set<Telemetry>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Device>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.SystemId).IsRequired();
            entity.HasMany(e => e.Sensors)
                .WithOne()
                .HasForeignKey(s => s.DeviceId)
                .OnDelete(DeleteBehavior.Cascade);
        });
        
        modelBuilder.Entity<Sensor>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Type).IsRequired();
            entity.Property(e => e.Zone).IsRequired();
        });
        
        modelBuilder.Entity<Telemetry>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.DeviceId).IsRequired();
            entity.Property(e => e.Timestamp).IsRequired();
            entity.Property(e => e.Temperature).IsRequired();
            entity.Property(e => e.AirHumidity).IsRequired();
            entity.Property(e => e.SoilHumidity).IsRequired();
        });
    }
}