namespace ToDo;

class Program
{
    private void Run()
    {
        while (true)
        {
            
            for (int i = 1; i <= Menu.Options.Count; i++)
            {
                Console.WriteLine($"{i}) {Menu.Options[i-1].Key}");
            }

            var userInput = Console.ReadLine();
            Console.WriteLine("____________________________________________________________________________________________________");
            var selectedOption = byte.TryParse(userInput, out var choice);
            if (!selectedOption || (choice < 1 || choice > Menu.Options.Count))
            {
                if (userInput == "exit")
                {
                    Console.WriteLine("Goodbye");
                    break;
                }
                Console.WriteLine("Please select a valid choice.");
                continue;
            }
            
            Menu.Options[choice-1].Value();
            Console.WriteLine();
        }
    }
    
    
    public static void Main()
    {
        var program = new Program();
        program.Run();
    }
} 


