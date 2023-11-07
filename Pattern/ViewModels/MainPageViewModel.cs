using System.Collections.ObjectModel;
using TaskApp.Pattern.Models;
using PropertyChanged; // Assuming this is used for property change notification
using System.Collections.Specialized;
using System.Linq;

namespace TaskApp.Pattern.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class MainPageViewModel
    {
        public ObservableCollection<Category> Categories { get; set; }
        public ObservableCollection<TaskItem> Tasks { get; set; }

        public MainPageViewModel()
        {
            // Initialize data and event handlers
            File_Info();
            Tasks.CollectionChanged += Tasks_CollectionChanged;

            // Initialize the Tasks collection within each Category
            foreach (var category in Categories)
            {
                category.Tasks = new ObservableCollection<TaskItem>(Tasks.Where(task => task.Cat_Id == category.Id));
                category.UpdateAllTasks();
            }
        }

        // Event handler for changes in the Tasks collection
        private void Tasks_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // Handle tasks collection changes
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (TaskItem task in e.NewItems)
                {
                    // Find the category associated with the added task
                    Category category = Categories.FirstOrDefault(c => c.Id == task.Cat_Id);
                    if (category != null)
                    {
                        // Update category data based on the added task
                        category.Tasks.Add(task);
                        category.Pending_Tasks = category.Tasks.Count(t => !t.CompletedTasks);
                        category.Percentage_Tasks = (float)category.Tasks.Count(t => t.CompletedTasks) / category.Tasks.Count;
                        category.UpdateAllTasks();
                    }
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (TaskItem task in e.OldItems)
                {
                    // Find the category associated with the removed task
                    Category category = Categories.FirstOrDefault(c => c.Id == task.Cat_Id);
                    if (category != null)
                    {
                        // Update category data based on the removed task
                        category.Tasks.Remove(task);
                        category.Pending_Tasks = category.Tasks.Count(t => !t.CompletedTasks);
                        category.Percentage_Tasks = (float)category.Tasks.Count(t => t.CompletedTasks) / category.Tasks.Count;
                        category.UpdateAllTasks();
                    }
                }
            }
        }

        // Initialize Categories and Tasks with hardcoded data
        private void File_Info()
        {
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
            Update_Info();
        }

        // Update data for categories and tasks
        public void Update_Info()
        {
            foreach (var c in Categories)
            {
                // Filter tasks for the current category
                var tasks = from t in Tasks
                            where t.Cat_Id == c.Id
                            select t;

                // Count completed and non-completed tasks
                var completed = from t in tasks
                                where t.CompletedTasks == true
                                select t;
                var noCompleted = from t in tasks
                                  where t.CompletedTasks == false
                                  select t;

                // Update category properties
                c.Pending_Tasks = noCompleted.Count();
                c.Percentage_Tasks = (float)completed.Count() / (float)tasks.Count();
            }

            foreach (var t in Tasks)
            {
                // Find the category color for the task
                var catColor =
                    (
                        from c in Categories
                        where c.Id == t.Cat_Id
                        select c.Color_Cat
                    ).FirstOrDefault();
                t.Task_Color = catColor;
            }
        }
    }
}
