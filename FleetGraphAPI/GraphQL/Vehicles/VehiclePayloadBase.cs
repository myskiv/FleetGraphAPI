using FleetGraphAPI.Common;
using FleetGraphAPI.Database.Models;

namespace FleetGraphAPI.GraphQL.Vehicles
{
    public class VehiclePayloadBase : Payload
    {
        public VehiclePayloadBase(Vehicle vehicle)
        {
            Vehicle = vehicle;
        }

        public VehiclePayloadBase(UserError error)
            : base(new[] { error })
        {
        }

        public Vehicle? Vehicle { get; }
    }
}
