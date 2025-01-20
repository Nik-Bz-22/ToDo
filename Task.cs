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
        
        public override string ToString()
        {
            return
                $"====================\nID: {Id}\n Title: {Title}\n Description: {Description}\n Deadline: {Deadline}\n Priority: {Priority}\n IsCompleted: {IsCompleted}\n";
        }
    }
}