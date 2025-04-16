namespace TaskCLI
{
    public class CommandHandler
    {
        private readonly TaskManager _taskManager;

        private readonly Dictionary<string, Action<string[]>> _commandActions;

        public CommandHandler(TaskManager taskManager)
        {
            _taskManager = taskManager;
            _commandActions = new Dictionary<string, Action<string[]>>(StringComparer.OrdinalIgnoreCase)
            {
                ["add"] = HandleAddCommand,
                ["update"] = HandleUpdateCommand,
                ["delete"] = HandleDeleteCommand,
                ["status"] = HandleStatusCommand,
                ["mark-in-progress"] = HandleMarkInProgressCommand,
                ["mark-done"] = HandleMarkDoneCommand,
                ["list"] = HandleListCommand
            };
        }

        public void ProcessCommand(string[] args)
        {
            string command = args[0].ToLower();

            if (_commandActions.TryGetValue(command, out var action))
            {
                action(args);
            }
            else
            {
                Console.WriteLine("Unknown command.");
            }
        }

        private void HandleAddCommand(string[] args)
        {
            if (args.Length > 1)
            {
                string description = string.Join(" ", args[1..]);
                _taskManager.AddTask(description);
            }
            else
            {
                Console.WriteLine("Please provide a task description.");
            }
        }

        private void HandleUpdateCommand(string[] args)
        {
            if (args.Length > 2 && TryParseTaskId(args[1], out int id))
            {
                string description = string.Join(" ", args[2..]);
                _taskManager.UpdateTaskDescription(id, description);
            }
            else
            {
                Console.WriteLine("Please provide a valid task ID and new description.");
            }
        }

        private void HandleDeleteCommand(string[] args)
        {
            if (args.Length > 1 && TryParseTaskId(args[1], out int id))
            {
                _taskManager.DeleteTask(id);
            }
            else
            {
                Console.WriteLine("Please provide a valid task ID.");
            }
        }

        private void HandleStatusCommand(string[] args)
        {
            if (args.Length > 2 && TryParseTaskId(args[1], out int id))
            {
                string newStatus = args[2];
                _taskManager.UpdateTaskStatus(id, newStatus);
            }
            else
            {
                Console.WriteLine("Please provide a valid task ID and new status.");
            }
        }

        private void HandleMarkInProgressCommand(string[] args)
        {
            if (args.Length > 1 && TryParseTaskId(args[1], out int id))
            {
                _taskManager.UpdateTaskStatus(id, "in-progress");
            }
            else
            {
                Console.WriteLine("Please provide a valid task ID.");
            }
        }

        private void HandleMarkDoneCommand(string[] args)
        {
            if (args.Length > 1 && TryParseTaskId(args[1], out int id))
            {
                _taskManager.UpdateTaskStatus(id, "done");
            }
            else
            {
                Console.WriteLine("Please provide a valid task ID.");
            }
        }

        private void HandleListCommand(string[] args)
        {
            if (args.Length > 1)
            {
                string statusFilter = args[1];
                _taskManager.ListTasksByStatus(statusFilter);
            }
            else
            {
                _taskManager.ListTasks();
            }
        }

        private bool TryParseTaskId(string input, out int taskId)
        {
            return int.TryParse(input, out taskId);
        }
    }
}
