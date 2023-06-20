using System.ComponentModel.DataAnnotations;

namespace FleetGraphAPI.Database.Models;

public class Vehicle
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(200)]
    public string Make { get; set; }
    
    [Required]
    [StringLength(200)]
    public string Model { get; set; }
    
    [Required]
    public int Year { get; set; }
    
    [Required]
    [StringLength(17)]
    public string VIN { get; set; }
    public string? CurrentLocation { get; set; }
    public float? FuelLevel { get; set; }
}