
using System.Text.Json;

namespace TaskCLI
{
    public class TaskManager
    {
        private const string FilePath = "tasks.json";

        private List<TaskModel> _tasks;

        public static readonly string[] ValidStatuses = { "Todo", "InProgress", "In-Progress", "Done" };

        public TaskManager()
        {
            _tasks = LoadTasks() ?? new List<TaskModel>();
        }

        public void AddTask(string description)
        {
            int id = _tasks.Any() ? _tasks.Max(t => t.Id) + 1 : 1;
            TaskModel task = new TaskModel(id, description);

            _tasks.Add(task);
            SaveTasks();

            Console.WriteLine($"Task added successfully (ID: {id})");
        }

        public void UpdateTaskDescription(int id, string description)
        {
            var task = _tasks.Find(t => t.Id == id);

            if(task != null)
            {
                task.Description = description;
                task.UpdatedAt = DateTime.Now;
                SaveTasks();
                Console.WriteLine($"Task updated successfully (ID: {id})");
            }
            else
            {
                Console.WriteLine($"Task with ID {id} not found.");
            } 
        }

        public void UpdateTaskStatus(int id, string status)
        {
            if(!ValidStatuses.Contains(status, StringComparer.OrdinalIgnoreCase))
            {
                Console.WriteLine($"Invalid status: {status}. Valid statuses are: {string.Join(", ", ValidStatuses)}");
                return;
            }

            var task = _tasks.Find(t => t.Id == id);

            if(task != null)
            {
                task.Status = status;
                task.UpdatedAt = DateTime.Now;
                SaveTasks();
                Console.WriteLine($"Task updated successfully (ID: {id})");
            }
            else
            {
                Console.WriteLine($"Task with ID {id} not found or invalid status.");
            }
        }

        public void DeleteTask(int id)
        {
            var task = _tasks.Find(t => t.Id == id);

            if (task != null)
            {
                _tasks.Remove(task);
                SaveTasks();
                Console.WriteLine($"Task deleted successfully (ID: {id})");
            }
            else
            {
                Console.WriteLine($"Task with ID {id} not found.");
            }
        }

        public void ListTasks()
        {
            if(_tasks.Any())
            {
                foreach (var task in _tasks)
                {
                    Console.WriteLine(task);
                }
            }
            else
            {
                Console.WriteLine("There are no tasks to display.");
            }
            
        }

        public void ListTasksByStatus(string status)
        {
            if (!ValidStatuses.Contains(status, StringComparer.OrdinalIgnoreCase))
            {
                Console.WriteLine($"Invalid status: {status}. Valid statuses are: {string.Join(", ", ValidStatuses)}");
                return;
            }

            IEnumerable<TaskModel> tasks = _tasks.Where(t =>
                 t.Status.Equals(status, StringComparison.OrdinalIgnoreCase)).ToList();

            if (tasks.Any())
            {
                foreach(var t in tasks)
                {
                    Console.WriteLine(t);
                }
            }
            else
            {
                Console.WriteLine("There are no tasks to display.");
            }
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

        private void SaveTasks()
        {
            try
            {
                using (FileStream fs = new FileStream(FilePath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    var jsonData = JsonSerializer.Serialize(_tasks, new JsonSerializerOptions { WriteIndented = true });
                    using (StreamWriter writer = new StreamWriter(fs))
                    {
                        writer.Write(jsonData);
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error saving tasks.");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
