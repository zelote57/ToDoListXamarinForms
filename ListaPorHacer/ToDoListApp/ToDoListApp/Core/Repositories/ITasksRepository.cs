using System.Threading.Tasks;
using ToDoListApp.Core.Domain;

namespace ToDoListApp.Core.Repositories
{
    public interface ITasksRepository : IDataBaseRepository<Tasks>
    {
        Task<Tasks> GetById(int id);
        Task<Tasks> GetLast();
    }
}
