using System;
using System.Threading.Tasks;
using ToDoListApp.Core;
using ToDoListApp.Core.Repositories;
using ToDoListApp.Helpers;
using ToDoListApp.Persistence.Repositories;

namespace ToDoListApp.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private TareasDatabase _context;
        private static readonly AsyncLock Mutex = new AsyncLock();

        public UnitOfWork(TareasDatabase context)
        {
            _context = context;
            Tasks = new TasksRepository(_context);
        }

        public ITasksRepository Tasks { get; }

        public async void Dispose()
        {
            using (await Mutex.LockAsync())
            {
                if (_context == null)
                {
                    return;
                }
                await Task.Factory.StartNew(() =>
                {
                    _context.GetConnection().Close();
                    _context.GetConnection().Dispose();
                    _context = null;
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                });
            }
        }
    }
}
