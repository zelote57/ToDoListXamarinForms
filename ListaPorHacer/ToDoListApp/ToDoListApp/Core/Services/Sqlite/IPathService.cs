namespace ToDoListApp.Core.Services.Sqlite
{
    public interface IPathService
    {
        string GetLocalFilePath(string filename);
    }
}
