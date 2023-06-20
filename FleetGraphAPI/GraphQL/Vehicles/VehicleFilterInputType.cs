using FleetGraphAPI.Database.Models;
using HotChocolate.Data.Filters;

namespace FleetGraphAPI.GraphQL.Vehicles
{
    public class VehicleFilterInputType : FilterInputType<Vehicle>
    {
        protected override void Configure(IFilterInputTypeDescriptor<Vehicle> descriptor)
        {
            descriptor.Ignore(t => t.Id);
            descriptor.Ignore(t => t.VIN);
        }
    }
}
