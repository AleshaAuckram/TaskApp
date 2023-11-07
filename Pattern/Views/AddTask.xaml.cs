using TaskApp.Pattern.Models;
using TaskApp.Pattern.ViewModels;
using System;
using System.Linq;

namespace TaskApp.Pattern.Views
{
    public partial class NewTaskView : ContentPage
    {
        public NewTaskView()
        {
            InitializeComponent();
        }

        private async void Task_Clicked(object sender, EventArgs e)
        {
            var vm = BindingContext as AddTaskViewModel;
            var selectedCategory = vm.Categories.Where(x => x.Selected_Tasks).FirstOrDefault();

            if (selectedCategory != null)
            {
                var task = new TaskItem
                {
                    Task_Name = vm.Task,
                    Cat_Id = selectedCategory.Id,
                    Task_Color = selectedCategory.Color_Cat
                };

                vm.Tasks.Add(task);

                // Manually call UpdateData from MainViewModel
                var mainViewModel = App.Current.MainPage.BindingContext as MainPageViewModel;
                mainViewModel.Update_Info();

                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Invalid Selection", "You must select a category", "OK");
            }
        }

        private async void Category_Clicked(object sender, EventArgs e)
        {
            var vm = BindingContext as AddTaskViewModel;

            string category = await DisplayPromptAsync("New Category", "Write the category name", maxLength: 50, keyboard: Keyboard.Text);

            if (!string.IsNullOrEmpty(category))
            {
                var random = new Random(); // Create a Random instance

                var newCategory = new Category
                {
                    Id = vm.Categories.Max(x => x.Id) + 1,
                    Color_Cat = Color.FromRgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255)).ToHex(), // Use 'random' to generate colors
                    CatName = category
                };
                vm.Categories.Add(newCategory);

                // Manually call UpdateData from MainViewModel
                var mainViewModel = App.Current.MainPage.BindingContext as MainPageViewModel;
                mainViewModel.Update_Info();
            }
        }
    }
}
