using FleetGraphAPI.Common;
using FleetGraphAPI.Database;
using FleetGraphAPI.Database.Models;
using FleetGraphAPI.Extensions;
using HotChocolate.Subscriptions;

namespace FleetGraphAPI.GraphQL.Vehicles
{
    [ExtendObjectType(OperationTypeNames.Mutation)]
    public class VehicleMutations
    {
        [UseApplicationDbContext]
        public async Task<VehiclePayloadBase> AddVehicleAsync(
            AddVehicleInput input,
            [ScopedService] ApplicationDbContext context,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(input.VIN))
            {
                return new VehiclePayloadBase(
                    new UserError("The VIN cannot be empty.", "VIN_EMPTY"));
            }

            var vehicle = new Vehicle
            {
                Make = input.Make,
                Model = input.Model,
                Year = input.Year,
                VIN = input.VIN,
            };

            context.Vehicles.Add(vehicle);
            await context.SaveChangesAsync(cancellationToken);

            return new VehiclePayloadBase(vehicle);
        }

        [UseApplicationDbContext]
        public async Task<VehiclePayloadBase> UpdateVehicleStatusAsync(
            UpdateVehicleStatusInput input,
            [ScopedService] ApplicationDbContext context,
            [Service] ITopicEventSender eventSender,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(input.Location))
            {
                return new VehiclePayloadBase(
                    new UserError("The location cannot be empty.", "LOCATION_EMPTY"));
            }

            var vehicle = await context.Vehicles.FindAsync(input.VehicleId);

            if (vehicle is null)
            {
                return new VehiclePayloadBase(
                    new UserError("Vehicle not found.", "VEHICLE_NOT_FOUND"));
            }

            vehicle.CurrentLocation = input.Location;
            vehicle.FuelLevel = input.FuelLevel;

            await context.SaveChangesAsync(cancellationToken);

            await eventSender.SendAsync(
                nameof(VehicleSubscriptions.OnVehicleStatusUpdated),
                vehicle.Id);

            return new VehiclePayloadBase(vehicle);
        }
    }
}
