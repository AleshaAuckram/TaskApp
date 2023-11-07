using PropertyChanged;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace TaskApp.Pattern.Models
{
    //Setting Category Properties
    [AddINotifyPropertyChangedInterface]
    public class Category
    {
        public int Id { get; set; }
        public string CatName { get; set; }
        public string Color_Cat { get; set; }
        public int Pending_Tasks { get; set; }
        public float Percentage_Tasks { get; set; }
        public bool Selected_Tasks { get; set; }
        public ObservableCollection<TaskItem> Tasks { get; set; }

        private int totalTasks;

        public int TotalTasks
        {
            get { return totalTasks; }
            set
            {
                if (totalTasks != value)
                {
                    totalTasks = value;
                }
            }
        }

        public Category()
        {
            // Initialize the Tasks collection and update total tasks
            Tasks = new ObservableCollection<TaskItem>();
            UpdateAllTasks();
        }

        // Method to add a task to this category
        public void CreateTask(TaskItem task)
        {
            Tasks.Add(task);
            UpdateAllTasks();
        }

        // Method to update the total number of tasks in this category
        public void UpdateAllTasks()
        {
            TotalTasks = Tasks.Count;
        }
    }
}
