using System;
using System.Collections.ObjectModel;
using PropertyChanged;
using TaskApp.Pattern.Models;

public class AddTaskViewModel
{
    public event EventHandler<TaskAddedEventArgs> TaskAdded;

    public string Task { get; set; }
    public ObservableCollection<TaskItem> Tasks { get; set; }
    public ObservableCollection<Category> Categories { get; set; }
    public Category Selected_Cat { get; set; }

    public AddTaskViewModel()
    {
        //Categories with sample data
        Categories = new ObservableCollection<Category>()
        {
                // Define categories with sample data
               new Category
            {
                Id = 1,
                CatName = "Inventory Management",
                Color_Cat = "#ff0000"
            },
            new Category
            {
                Id = 2,
                CatName = "Order Processing",
                Color_Cat = "#008000"
            },
            new Category
            {
                Id = 3,
                CatName = "Customer Interaction",
                Color_Cat = "#007BFF"
            }
            };

        Tasks = new ObservableCollection<TaskItem>()
            {
                // Define tasks with sample data
                new TaskItem
            {
                Task_Name = "10 x Eggs",
                CompletedTasks = false,
                Cat_Id = 1,
            },
            new TaskItem
            {
                Task_Name = "5 x Milk",
                CompletedTasks = false,
                Cat_Id = 1,
            },
            new TaskItem
            {
                Task_Name = "Order #9207 Online",
                CompletedTasks = false,
                Cat_Id = 2,
            },
            new TaskItem
            {
                Task_Name = "Order #1209 Online",
                CompletedTasks = false,
                Cat_Id = 2,
            },
            new TaskItem
            {
                Task_Name = "Order #1352 Online",
                CompletedTasks = false,
                Cat_Id = 2,
            },
            new TaskItem
            {
                Task_Name = "Parsly Bread, Strawberry Cake",
                CompletedTasks = false,
                Cat_Id = 3,
            },
            new TaskItem
            {
                Task_Name = "Buttermilk Pie, Canberry Pie, Cheese Scone ",
                CompletedTasks = false,
                Cat_Id = 3,
            }
        };
    }

    public class TaskAddedEventArgs : EventArgs
    {
        public TaskItem NewTask { get; }

        public TaskAddedEventArgs(TaskItem newTask)
        {
            NewTask = newTask;
        }
    }

    //Method to add tasks
   public void AddTask()
    {
        if (Selected_Cat != null)
        {
            var newTask = new TaskItem { Task_Name = Task, CompletedTasks = false, Cat_Id = Selected_Cat.Id };
            Selected_Cat.CreateTask(newTask);
            TaskAdded?.Invoke(this, new TaskAddedEventArgs(newTask));
            Task = string.Empty;
        }
    }
}
