using SQLite;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ToDoListApp.Core.Repositories;

namespace ToDoListApp.Persistence.Repositories
{
    public class DataBaseRepository<TEntity> : IDataBaseRepository<TEntity> where TEntity : class, new()
    {
        protected readonly TareasDatabase Context;
        private readonly AsyncTableQuery<TEntity> _entities;

        public DataBaseRepository(TareasDatabase context)
        {
            Context = context;
            Context.CreateTableAsync<TEntity>().Wait();
            _entities = Context.Table<TEntity>();
        }

        public async Task<TEntity> Get(int id) => await Context.FindAsync<TEntity>(id);

        public async Task<List<TEntity>> GetAll() => await _entities.ToListAsync();

        public async Task<List<TEntity>> Find(Expression<Func<TEntity, bool>> predicate) =>
            await _entities.Where(predicate).ToListAsync();

        public async Task<TEntity> SingleOrDefault(Expression<Func<TEntity, bool>> predicate) =>
            await _entities.Where(predicate).FirstOrDefaultAsync();

        public async Task<int> Add(TEntity entity) => await Context.InsertAsync(entity);

        public async Task<int> AddRange(IEnumerable<TEntity> entities)
        {
            var count = 0;
            await Context.RunInTransactionAsync(connection =>
            {
                count += connection.InsertAll(entities);
            });
            return count;
        }

        public async Task<int> Remove(TEntity entity) => await Context.DeleteAsync(entity);

        public async Task<int> RemoveRange()
        {
            var count = 0;
            await Context.RunInTransactionAsync(connection =>
            {
                count += connection.DeleteAll<TEntity>();
            });
            return count;
        }

    }
}
