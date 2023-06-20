using FleetGraphAPI.Database;
using FleetGraphAPI.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace FleetGraphAPI.DataLoader
{
    public class VehicleByIdDataLoader : BatchDataLoader<int, Vehicle>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

        public VehicleByIdDataLoader(
            IDbContextFactory<ApplicationDbContext> dbContextFactory,
            IBatchScheduler batchScheduler,
            DataLoaderOptions options)
            : base(batchScheduler, options)
        {
            _dbContextFactory = dbContextFactory ??
                throw new ArgumentNullException(nameof(dbContextFactory));
        }

        protected override async Task<IReadOnlyDictionary<int, Vehicle>> LoadBatchAsync(
            IReadOnlyList<int> keys,
            CancellationToken cancellationToken)
        {
            await using ApplicationDbContext dbContext =
                _dbContextFactory.CreateDbContext();

            return await dbContext.Vehicles
                .Where(s => keys.Contains(s.Id))
                .ToDictionaryAsync(t => t.Id, cancellationToken);
        }
    }
}
