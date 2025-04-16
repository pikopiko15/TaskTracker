using TaskCLI;

internal class Program
{
    private static void Main(string[] args)
    {
        TaskManager taskManager = new TaskManager();

        CommandHandler commandHandler = new CommandHandler(taskManager);

        if (args.Length == 0)
        {
            Console.WriteLine("Please provide a command.");
            return;
        }

        commandHandler.ProcessCommand(args);
    }
}
