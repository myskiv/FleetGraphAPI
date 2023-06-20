using FleetGraphAPI.Database;
using FleetGraphAPI.Database.Models;
using FleetGraphAPI.DataLoader;
using FleetGraphAPI.Extensions;
using FleetGraphAPI.GraphQL.Vehicles;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace FleetGraphAPI.GraphQL.Alerts
{
    //[Authorize(Roles = new[] { "Admin" })]
    [ExtendObjectType(OperationTypeNames.Query)]
    public class VehicleQueries
    {
        [UseApplicationDbContext]
        [UsePaging]
        [UseFiltering(typeof(VehicleFilterInputType))]
        [UseSorting]
        public IQueryable<Vehicle> GetVehicles(
            [ScopedService] ApplicationDbContext context)
            => context.Vehicles;

        public Task<Vehicle> GetVehicleByIdAsync(
            [ID(nameof(Vehicle))] int id,
            VehicleByIdDataLoader vehicleById,
            CancellationToken cancellationToken)
            => vehicleById.LoadAsync(id, cancellationToken);

        public async Task<IEnumerable<Vehicle>> GetVehiclesByIdAsync(
            [ID(nameof(Vehicle))] int[] ids,
            VehicleByIdDataLoader vehicleById,
            CancellationToken cancellationToken)
            => await vehicleById.LoadAsync(ids, cancellationToken);
    }
}
