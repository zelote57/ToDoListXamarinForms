using System;
using ToDoListApp.Core.Repositories;

namespace ToDoListApp.Core
{
    public interface IUnitOfWork : IDisposable
    {
        ITasksRepository Tasks { get; }
    }
}
