
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

        public void AddTask(string description)
        {
            int id = _tasks.Count + 1;
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

        public void UpdateTaskStatus(int id, Status status)
        {
            var task = _tasks.Find(t => t.Id == id);

            if(task != null)
            {
                task.Status = status;
                task.UpdatedAt = DateTime.Now;
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
            foreach(var task in _tasks)
            {
                Console.WriteLine(task);
            }
        }

        public void ListTasksByStatus(Status status)
        {
            IEnumerable<TaskModel> tasks = _tasks.Where(t => t.Status == status).ToList();

            if(tasks.Any())
            {
                foreach(var t in tasks)
                {
                    Console.WriteLine(t);
                }
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
                var jsonData = JsonSerializer.Serialize(_tasks, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(FilePath, jsonData);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error saving tasks.");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
