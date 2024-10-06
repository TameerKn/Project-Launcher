using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Project_C_.Games.MyTasks
{
    internal class TaskManagerService
    {
        public ObservableCollection<TaskModel> Tasks { get; set; }
        private const string FilePath = "tasks.json";

        public TaskManagerService()
        {
            Tasks = new ObservableCollection<TaskModel>();
            LoadTasks();
        }

        public void SaveTasks()
        {
            string jsonString = JsonSerializer.Serialize(Tasks);
            File.WriteAllText(FilePath, jsonString);
        }

        public void LoadTasks()
        {
            if (File.Exists(FilePath))
            {
                string jsonString = File.ReadAllText(FilePath);
                var tasks = JsonSerializer.Deserialize<List<TaskModel>>(jsonString);
                if (tasks != null)
                {
                    foreach (var task in tasks)
                    {
                        Tasks.Add(task);
                    }
                }
            }
        }

        public void UpdateTask(int taskId, string newDescription)
        {
            TaskModel task = Tasks.FirstOrDefault(task => task.Id == taskId);
            if (task != null)
            {
                task.Description = newDescription;
                SaveTasks();
            }
            else
            {
                throw new Exception("The task with this Id wasn't found");
            }
        }
        public void ToggleTaskIsComplete(int taskId)
        {
            TaskModel task = Tasks.FirstOrDefault(task => task.Id == taskId);
            if (task != null)
            {
                if (!task.IsCompleted)
                {
                    task.IsCompleted = false;
                    SaveTasks();
                } else
                {
                    task.IsCompleted = true;
                    SaveTasks();
                }

            }
            else
            {
                throw new Exception("The task with this Id wasn't found");
            }
        }
        public void AddNewTask(TaskModel task)
        {
            Tasks.Add(task);
            SaveTasks();
        }

        public void RemoveTask(TaskModel task)
        {
            Tasks.Remove(task);
            SaveTasks();
        }



    }
}
