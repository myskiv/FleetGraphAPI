using System.ComponentModel.DataAnnotations;

namespace FleetGraphAPI.Database.Models;

public class Alert
{
    public int Id { get; set; }
    public int VehicleId { get; set; }
    
    [StringLength(200)]
    public string AlertType { get; set; }
    public DateTime Timestamp { get; set; }
    
    public Vehicle Vehicle { get; set; }
}