namespace ToDoListApp.Core.Domain
{
    public class BaseEntity
    {
        [SQLite.AutoIncrement, SQLite.PrimaryKey]
        public int Id { get; set; }
    }
}
