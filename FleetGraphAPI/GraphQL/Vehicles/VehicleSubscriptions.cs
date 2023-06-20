using FleetGraphAPI.Database.Models;
using FleetGraphAPI.DataLoader;

namespace FleetGraphAPI.GraphQL.Vehicles
{
    [ExtendObjectType(OperationTypeNames.Subscription)]
    public class VehicleSubscriptions
    {
        [Subscribe]
        [Topic]
        public Task<Vehicle> OnVehicleStatusUpdated(
        [EventMessage] int vehicleId,
        VehicleByIdDataLoader vehicleById,
        CancellationToken cancellationToken)
        => vehicleById.LoadAsync(vehicleId, cancellationToken);
    }
}
