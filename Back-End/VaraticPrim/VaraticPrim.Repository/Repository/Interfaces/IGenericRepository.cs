using VaraticPrim.Domain.Entities;

namespace VaraticPrim.Repository.Repository.Interfaces;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<T?>             GetById(int id);
    IQueryable<T>        Table { get; }
    Task<IEnumerable<T>> GetAll();
    Task                 Insert(T entity);
    Task                 InsertRange(IEnumerable<T> entities);
    Task                 Update(T entity);
    Task                 UpdateRange(IEnumerable<T> entities);
    Task                 Delete(T entity);
    Task                 DeleteRange(IEnumerable<T> entities);
}