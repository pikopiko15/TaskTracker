using TaskCLI;

internal class Program
{
    private static void Main(string[] args)
    {
        TaskManager taskManager = new TaskManager();

        if (args.Length == 0)
        {
            Console.WriteLine("Please provide a command.");
            return;
        }

        string command = args[0].ToLower();

        switch (command)
        {
            case "add":
                if (args.Length > 1)
                {
                    string description = string.Join(" ", args[1..]);
                    taskManager.AddTask(description);
                }
                else
                {
                    Console.WriteLine("Please provide a task description.");
                }
                break;

            case "update":
                if (args.Length > 2 && int.TryParse(args[1], out int id))
                {
                    string description = string.Join(" ", args[2..]);
                    taskManager.UpdateTaskDescription(id, description);
                }
                else
                {
                    Console.WriteLine("Please provide a valid task ID and new description.");
                }
                break;

            case "delete":
                if (args.Length > 1 && int.TryParse(args[1], out int deleteId))
                {
                    taskManager.DeleteTask(deleteId);
                }
                else
                {
                    Console.WriteLine("Please provide a valid task ID.");
                }
                break;

            case "status":
                if (args.Length > 2 && int.TryParse(args[1], out int statusId))
                {
                    string newStatus = args[2];
                    taskManager.UpdateTaskStatus(statusId, newStatus);
                }
                else
                {
                    Console.WriteLine("Please provide a valid task ID and new status.");
                }
                break;

            case "mark-in-progress":
                if (args.Length > 1 && int.TryParse(args[1], out int inProgressId))
                {
                    taskManager.UpdateTaskStatus(inProgressId, "in-progress");
                }
                else
                {
                    Console.WriteLine("Please provide a valid task ID.");
                }
                break;

            case "mark-done":
                if (args.Length > 1 && int.TryParse(args[1], out int doneId))
                {
                    taskManager.UpdateTaskStatus(doneId, "done");
                }
                else
                {
                    Console.WriteLine("Please provide a valid task ID.");
                }
                break;

            case "list":
                if (args.Length > 1)
                {
                    string statusFilter = args[1];
                    taskManager.ListTasksByStatus(statusFilter);
                }
                else
                {
                    taskManager.ListTasks();
                }
                break;

            default:
                Console.WriteLine("Unknown command.");
                break;
        }
    }
}
