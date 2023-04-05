using VaraticPrim.Repository.Entity;

namespace VaraticPrim.Repository.Repository;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<T?>             GetById(int id);
    IQueryable<T>        Table { get; }
    Task<IEnumerable<T>> GetAll();
    Task                 Insert(T entity, bool trigger = true);
    Task                 InsertRange(IEnumerable<T> entities);
    Task                 Update(T entity, bool trigger = true);
    Task                 UpdateRange(IEnumerable<T> entities);
    Task                 Delete(T entity, bool trigger = true);
    Task                 DeleteRange(IEnumerable<T> entities);
}