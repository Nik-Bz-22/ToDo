namespace ToDo;

public static class Config
{
    public static readonly string ROOT_DIR = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
    public static class TaskStatus
    {
        public static readonly string COMPLETED = "Completed";
        public static readonly string IN_PROGRESS = "In progress";
        public static readonly string EXPIRED = "Expired";
    }
}