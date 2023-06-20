using FleetGraphAPI.Database;

namespace FleetGraphAPI.Extensions
{
    public class UseApplicationDbContextAttribute : UseDbContextAttribute
    {
        public UseApplicationDbContextAttribute() : base(typeof(ApplicationDbContext))
        {
        }
    }
}
