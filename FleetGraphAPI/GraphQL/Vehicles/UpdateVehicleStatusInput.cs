namespace FleetGraphAPI.GraphQL.Vehicles
{
    public record UpdateVehicleStatusInput(
        int VehicleId,
        string Location,
        float FuelLevel);
}
