namespace FleetGraphAPI.GraphQL.Vehicles
{
    public record AddVehicleInput(
        string Make,
        string Model,
        int Year,
        string VIN);
}
