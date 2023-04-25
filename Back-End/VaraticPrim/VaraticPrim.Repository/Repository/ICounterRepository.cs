using VaraticPrim.Domain.Entity;

namespace VaraticPrim.Repository.Repository;

public interface ICounterRepository : IGenericRepository<CounterEntity>
{
    public Task<bool> CounterExists(string barCode);
}