using System.Threading.Tasks;
using ToDoListApp.Core.Domain;
using ToDoListApp.Core.Repositories;
using System.Linq;
using System.Collections.Generic;

namespace ToDoListApp.Persistence.Repositories
{
    public class TasksRepository : DataBaseRepository<Tasks>, ITasksRepository
    {
        public TasksRepository(TareasDatabase context) : base(context)
        {
        }

        public async Task<Tasks> GetById(int id)
        {
            return await SingleOrDefault(x => x.Id.Equals(id));
        }

        public async Task<Tasks> GetLast()
        {
            List<Tasks> res = await GetAll();
            return await Task.FromResult(res.OrderByDescending(i => i.Id).Take(1).SingleOrDefault());
        }
    }
}
