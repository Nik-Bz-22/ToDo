namespace ToDo
{

    static class Menu
    {
        public static List<KeyValuePair<string, Action>> Options { get; }

        static Menu()
        {
            Options = new List<KeyValuePair<string, Action>>
            {
                new KeyValuePair<string, Action>("Show all tasks", () => Menu.DisplayAllTasks()),
                new KeyValuePair<string, Action>("Add new task", () => Menu.AddTask()),
                new KeyValuePair<string, Action>("Mark task as complete", () => Menu.MarkAsComplete()),
                new KeyValuePair<string, Action>("Delete task", () => Menu.DeleteTask())
            };
        }

        public static List<Task> GetAllTasks()
        {
            using (var db = new AppDbContext())
            {
                var tasks = db.Task.ToList();
                return tasks;
            }
        }

        public static void AddTask()
        {
            // validate task
            Console.WriteLine("Please enter the name of the task you would like to add:");
            var title = Console.ReadLine();
            Console.WriteLine("Please enter the description of the task you would like to add:");
            var description = Console.ReadLine();
            Console.WriteLine("Please enter the due date of the task you would like to add:");
            var dueDate = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Please enter the priority of the task you would like to add:");
            var priority = byte.Parse(Console.ReadLine());

            using (var db = new AppDbContext())
            {
                db.Task.Add(new Task
                {
                    Title = title, Description = description, IsCompleted = false, Deadline = dueDate,
                    Priority = priority
                });
                db.SaveChanges();
            }
            Console.WriteLine("New task added and saved to db.");
        }

        public static void DisplayAllTasks()
        {
            var tasks = GetAllTasks();
            Console.WriteLine($"Tasks found: {tasks.Count}");
            foreach (var task in tasks)
            {
                Console.WriteLine(task);
            }
        }

        public static void MarkAsComplete()
        {
            Console.WriteLine("Select the task you would like to mark as completed(ID):");
            var selectedTaskID = int.Parse(Console.ReadLine().Trim());
            using (var db = new AppDbContext())
            {
                try
                {
                    var task = db.Task.Find(selectedTaskID);
                    task.IsCompleted = true;
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        public static void DeleteTask()
        {
            Console.WriteLine("Select the task you would like to delete(ID):");
            var selectedTaskID = int.Parse(Console.ReadLine().Trim());
            using (var db = new AppDbContext())
            {
                try
                {
                    var task = db.Task.Find(selectedTaskID);
                    db.Task.Remove(task);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
}