using VaraticPrim.Domain.Entities;
using VaraticPrim.Repository.Persistence;
using VaraticPrim.Repository.Repository.Interfaces;

namespace VaraticPrim.Repository.Repository.Implementations;

public class LocationRepository : GenericRepository<LocationEntity>, ILocationRepository
{
    public LocationRepository(ApplicationDbContext context) : base(context)
    {
    }
}