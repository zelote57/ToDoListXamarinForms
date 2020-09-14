using System.IO;
using ToDoListApp.Core.Services.Sqlite;
using ToDoListApp.Droid.Services.Sqlite;
using Xamarin.Forms;

[assembly: Dependency(typeof(PathService))]
namespace ToDoListApp.Droid.Services.Sqlite
{
    public class PathService : IPathService
    {
        public string GetLocalFilePath(string filename)
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string dbPath = Path.Combine(path, filename);
            CopyDatabaseIfNotExists(dbPath, filename);
            return dbPath;
        }

        private static void CopyDatabaseIfNotExists(string dbPath, string filename)
        {
            if (!File.Exists(dbPath))
            {
                using (var br = new BinaryReader(Android.App.Application.Context.Assets.Open(filename)))
                {
                    using (var bw = new BinaryWriter(new FileStream(dbPath, FileMode.Create)))
                    {
                        byte[] buffer = new byte[2048];
                        int length = 0;
                        while ((length = br.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            bw.Write(buffer, 0, length);
                        }
                    }
                }
            }
        }
    }
}