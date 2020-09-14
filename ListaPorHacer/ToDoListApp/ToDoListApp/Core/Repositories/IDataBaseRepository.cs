using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ToDoListApp.Core.Repositories
{
    public interface IDataBaseRepository<TEntity> where TEntity : class
    {
        Task<TEntity> Get(int id);

        Task<List<TEntity>> GetAll();

        Task<List<TEntity>> Find(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> SingleOrDefault(Expression<Func<TEntity, bool>> predicate);

        Task<int> Add(TEntity entity);

        Task<int> AddRange(IEnumerable<TEntity> entities);

        Task<int> Remove(TEntity entity);

        Task<int> RemoveRange();
    }
}
