using System.ComponentModel.DataAnnotations;

namespace FleetGraphAPI.Database.Models;

public class Trip
{
    public int Id { get; set; }
    public int VehicleId { get; set; }
    
    [StringLength(200)]
    public string StartLocation { get; set; }
    
    [StringLength(200)]
    public string EndLocation { get; set; }
    public TimeSpan Duration { get; set; }
    public float Distance { get; set; }
    public float AverageSpeed { get; set; }
    
    public Vehicle Vehicle { get; set; }
}