using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_C_.Games.MyTasks
{
    internal class TaskModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreationTime { get; set; }
        public TaskModel(int id, string description)
        {
            this.Id = id;
            this.Description = description;
            this.IsCompleted = false;
            this.CreationTime = DateTime.Now;

        }

        public string DisplayText
        {
            get
            {
                return $"{Description}";
            }
        }

    }
}
