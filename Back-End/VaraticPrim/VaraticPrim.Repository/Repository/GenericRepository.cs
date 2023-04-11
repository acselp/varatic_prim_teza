using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using VaraticPrim.Domain.Entity;
using VaraticPrim.Repository.Persistance;

namespace VaraticPrim.Repository.Repository;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity, new()
{
    protected readonly ApplicationDbContext Context;
 
    public GenericRepository(ApplicationDbContext context)
    {
        Context = context;
    }
 
    public async Task<T?> GetById(int id)
        => await Context.Set<T>().FindAsync(id);
 
    public IQueryable<T> Table =>
        Context.Set<T>();
 
    public async Task<T?> Find(Expression<Func<T, bool>> predicate)
        => await Context.Set<T>().FirstOrDefaultAsync(predicate);
 
    public async Task<IEnumerable<T>> GetAll()
        => await Context.Set<T>().ToListAsync();
 
    public async Task Insert(T entity, bool trigger = true)
    {
        entity.CreatedOnUtc = DateTime.UtcNow;
        entity.UpdatedOnUtc = DateTime.UtcNow;
 
        Context.Set<T>().Add(entity);
        await Context.SaveChangesAsync();
    }
 
    public async Task InsertRange(IEnumerable<T> entities)
    {
        Context.Set<T>().AddRange(entities);
        await Context.SaveChangesAsync();
    }
 
    public async Task Update(T entity, bool trigger = true)
    {
        entity.UpdatedOnUtc = DateTime.UtcNow;
 
        Context.Set<T>().Update(entity);
        await Context.SaveChangesAsync();
    }
 
    public async Task UpdateRange(IEnumerable<T> entities)
    {
        Context.Set<T>().UpdateRange(entities);
        await Context.SaveChangesAsync();
    }
 
    public async Task Delete(T entity, bool trigger = true)
    {
        Context.Set<T>().Remove(entity);
        await Context.SaveChangesAsync();
    }
 
    public async Task DeleteRange(IEnumerable<T> entities)
    {
        Context.Set<T>().RemoveRange(entities);
        await Context.SaveChangesAsync();
    }
}