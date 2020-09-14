using System;
using System.IO;
using Foundation;
using ToDoListApp.Core.Services.Sqlite;
using ToDoListApp.iOS.Services.Sqlite;
using Xamarin.Forms;

[assembly: Dependency(typeof(PathService))]
namespace ToDoListApp.iOS.Services.Sqlite
{
    public class PathService : IPathService
    {
        public string GetLocalFilePath(string filename)
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            string dbPath = Path.Combine(libFolder, filename);

            CopyDatabaseIfNotExists(dbPath, filename);

            return dbPath;
        }

        private static void CopyDatabaseIfNotExists(string dbPath, string filename)
        {
            if (!File.Exists(dbPath))
            {
                var existingDb = NSBundle.MainBundle
                    .PathForResource(filename.Substring(0, filename.IndexOf(".", StringComparison.InvariantCulture)), "db3");
                File.Copy(existingDb, dbPath);
            }
        }
    }
}