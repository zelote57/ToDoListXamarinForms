namespace ToDoListApp.Core.Domain
{
    public class Tasks : BaseEntity
    {
        public string TaskName { get; set; }
        public string Status { get; set; }
    }
}
