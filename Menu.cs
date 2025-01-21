using System.Globalization;

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
                new KeyValuePair<string, Action>("Delete task", () => Menu.DeleteTask()),
                new KeyValuePair<string, Action>("Show active tasks", () => Menu.DisplayActiveTasks()),
                
            };
        }

        private static List<Task> GetAllTasks()
        {
            using (var db = new AppDbContext())
            {
                var tasks = db.Task.ToList();
                return tasks;
            }
        }

        public static void AddTask()
        {
            string title = null;
            string description = null;
            DateTime deadline;
            byte priority;

            while (string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine("Please enter the name of the task you would like to add:");
                title = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(title))
                {
                    Console.WriteLine("Task name cannot be empty. Please try again.");
                }
            }

            while (string.IsNullOrWhiteSpace(description))
            {
                Console.WriteLine("Please enter the description of the task you would like to add:");
                description = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(description))
                {
                    Console.WriteLine("Task description cannot be empty. Please try again.");
                }
            }

            while (true)
            {
                Console.WriteLine("Please enter the due date of the task you would like to add (dd/MM/yyyy HH:mm:ss):");
                string input = Console.ReadLine();
                if (DateTime.TryParseExact(input, "dd/MM/yyyy HH:mm:ss", null, DateTimeStyles.None, out deadline))
                {
                    break;
                }
                Console.WriteLine("Invalid date format. Please use dd/MM/yyyy HH:mm:ss.");
            }

            while (true)
            {
                Console.WriteLine("Please enter the priority of the task you would like to add (0-255):");
                string input = Console.ReadLine();
                if (byte.TryParse(input, out priority))
                {
                    break;
                }
                Console.WriteLine("Invalid priority. Please enter a number between 0 and 255.");
            }

            using (var db = new AppDbContext())
            {
                db.Task.Add(new Task(title, description, false, deadline, priority));
                db.SaveChanges();
            }

            Console.WriteLine("New task added and saved to the database.");
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

        public static void DisplayActiveTasks()
        {
            var inProgressTasks = new List<Task>();

            using (var db = new AppDbContext())
            {
                try
                {
                    inProgressTasks = db.Task.Where(t => t.Status == Config.TaskStatus.IN_PROGRESS).ToList();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    inProgressTasks = new List<Task>(); 
                }
            }

            if (inProgressTasks.Count > 0)
            {
                foreach (var task in inProgressTasks)
                {
                    Console.WriteLine(task);
                }
            }
            else
            {
                Console.WriteLine("No active tasks found");
            }
            
        }

    }
}