namespace ToDo
{

    public class Task
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime Deadline { get; set; }
        public byte Priority { get; set; }
        public int Id { get; set; }
        public string Status {get; set;}

        
        public Task(string title, string description, bool isCompleted, DateTime deadline, byte priority)
        {
            Title = title;
            Description = description;
            IsCompleted = isCompleted;
            Deadline = deadline;
            Priority = priority;
            Status = CalculateStatus();
        }

        private string CalculateStatus()
        {
            if (IsCompleted)
            {
                return Config.TaskStatus.COMPLETED;
            }
            else if (DateTime.Now > Deadline)
            {
                return Config.TaskStatus.EXPIRED;
            }
            else
            {
                return Config.TaskStatus.IN_PROGRESS;
            }
        }
        
        public void UpdateStatus()
        {
            Status = CalculateStatus();
        }
        
        
        
        public override string ToString()
        {
            return
                $"====================\nID: {Id}\n Title: {Title}\n Description: {Description}\n Deadline: {Deadline}\n Priority: {Priority}\n IsCompleted: {IsCompleted}\n Status: {Status}";
        }
    }
}