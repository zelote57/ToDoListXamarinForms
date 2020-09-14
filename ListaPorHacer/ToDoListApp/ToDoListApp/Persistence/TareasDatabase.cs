using SQLite;
using ToDoListApp.Core.Services.Sqlite;

namespace ToDoListApp.Persistence
{
    public class TareasDatabase : SQLiteAsyncConnection
    {
        private static readonly string Path = Xamarin.Forms.DependencyService.Get<IPathService>()
                                               .GetLocalFilePath(nameof(TareasDatabase).ToLower() + ".db3");
        public TareasDatabase() : base(Path)
        {
        }
    }
}
