using TaskCLI;

internal class Program
{
    private static void Main(string[] args)
    {
        TaskManager taskManager = new TaskManager();

        if(args.Length == 0)
        {
            Console.WriteLine("Please provide a command.");
            return;
        }

        string command = args[0].ToLower();
    }
}