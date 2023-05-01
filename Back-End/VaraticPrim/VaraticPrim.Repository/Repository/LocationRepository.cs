using VaraticPrim.Domain.Entities;
using VaraticPrim.Repository.Persistence;

namespace VaraticPrim.Repository.Repository;

public class LocationRepository : GenericRepository<LocationEntity>, ILocationRepository
{
    public LocationRepository(ApplicationDbContext context) : base(context)
    {
    }
}