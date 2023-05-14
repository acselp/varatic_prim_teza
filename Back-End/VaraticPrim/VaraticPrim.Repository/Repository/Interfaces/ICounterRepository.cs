using VaraticPrim.Domain.Entities;

namespace VaraticPrim.Repository.Repository.Interfaces;

public interface ICounterRepository : IGenericRepository<CounterEntity>
{
    public Task<bool> CounterExists(string barCode);
    public Task<bool> CounterExists(int id);
    public Task<CounterEntity?> GetByBarCode(string barCode);
}