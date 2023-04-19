using VaraticPrim.Domain.Entity;
using VaraticPrim.Repository.Persistance;

namespace VaraticPrim.Repository.Repository;

public class CounterRepository : GenericRepository<CounterEntity>, ICounterRepository
{
    public CounterRepository(ApplicationDbContext context) : base(context)
    {
    }
}