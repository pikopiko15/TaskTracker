
using System.Text.Json;

namespace TaskCLI
{
    public class TaskManager
    {
        private const string FilePath = "tasks.json";
        private List<TaskModel> _tasks;

        public TaskManager()
        {
            _tasks = LoadTasks() ?? new List<TaskModel>();
        }

        private List<TaskModel>? LoadTasks()
        {
            try
            {
                if (!File.Exists(FilePath))
                {
                    File.WriteAllText(FilePath, "[]");
                    return new List<TaskModel>();
                }

                string jsonData = File.ReadAllText(FilePath);

                if (string.IsNullOrEmpty(jsonData))
                {
                    return new List<TaskModel>();
                }

                return JsonSerializer.Deserialize<List<TaskModel>>(jsonData);
            }
            catch (JsonException ex)
            {
                Console.WriteLine("Error loading tasks: Invalid JSON format. Resetting tasks.json.");
                Console.WriteLine(ex.Message);
                File.WriteAllText(FilePath, "[]");

                return new List<TaskModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading tasks.");
                Console.WriteLine(ex.Message);

                return new List<TaskModel>();
            }
        }
    }
}
