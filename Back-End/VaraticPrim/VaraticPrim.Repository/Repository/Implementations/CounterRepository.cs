using Microsoft.EntityFrameworkCore;
using VaraticPrim.Domain.Entities;
using VaraticPrim.Repository.Persistence;
using VaraticPrim.Repository.Repository.Interfaces;

namespace VaraticPrim.Repository.Repository.Implementations;

public class CounterRepository : GenericRepository<CounterEntity>, ICounterRepository
{
    public CounterRepository(ApplicationDbContext context) : base(context)
    {
    }
    
    public async Task<bool> CounterExists(string barCode)
    {
        return await Table.AnyAsync(it => it.Barcode == barCode.ToLower().Trim());
    }
    
    public async Task<bool> CounterExists(int id)
    {
        return await Table.AnyAsync(it => it.Id == id);
    }
    
    public async Task<CounterEntity?> GetByBarCode(string barCode)
    {
        return await Table.FirstOrDefaultAsync(it => it.Barcode == barCode.ToLower().Trim());
    }
}