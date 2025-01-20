using System.Collections.Generic;
using System.IO;
using System;



namespace ToDo
{

    class Program
    {
        private Dictionary<byte, KeyValuePair<string, Action>> MenuOptions { get; }
        private Program()
        {
            MenuOptions = new Dictionary<byte, KeyValuePair<string, Action>>();

            MenuOptions[1] = new KeyValuePair<string, Action>("Show all tasks", () => Menu.DisplayAllTasks());
            MenuOptions[2] = new KeyValuePair<string, Action>("Add new task", () => Menu.AddTask());
            MenuOptions[3] = new KeyValuePair<string, Action>("Mark task as complete", () => Menu.MarkAsComplete());
            MenuOptions[4] = new KeyValuePair<string, Action>("Delete task", () => Menu.DeleteTask());
        }



        private void Run()
        {
            while (true)
            {
                
                foreach (var option in MenuOptions)
                {
                    Console.WriteLine($"{option.Key}) {option.Value.Key}");
                }

                var userInput = Console.ReadLine();
                var selectedOption = byte.TryParse(userInput, out var choice);
                if (!selectedOption || (choice < 1 || choice > MenuOptions.Count))
                {
                    if (userInput == "exit")
                    {
                        Console.WriteLine("Goodbye");
                        break;
                    }
                    Console.WriteLine("Please select a valid choice.");
                    continue;
                }
                
                MenuOptions[choice].Value();
                Console.WriteLine();
            }
        }
        
        
        

        public static void Main()
        {
            var program = new Program();
            program.Run();
        }
    } 
}

