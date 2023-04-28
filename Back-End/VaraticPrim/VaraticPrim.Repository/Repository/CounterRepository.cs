using Microsoft.EntityFrameworkCore;
using VaraticPrim.Domain.Entities;
using VaraticPrim.Repository.Persistence;

namespace VaraticPrim.Repository.Repository;

public class CounterRepository : GenericRepository<CounterEntity>, ICounterRepository
{
    public CounterRepository(ApplicationDbContext context) : base(context)
    {
    }
    
    public async Task<bool> CounterExists(string barCode)
    {
        return await Table.AnyAsync(it => it.Barcode == barCode.ToLower().Trim());
    }
}