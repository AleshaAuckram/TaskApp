using TaskApp.Pattern.ViewModels;

namespace TaskApp.Pattern.Views;

public partial class MainView : ContentPage
{
    private MainPageViewModel _mainViewModel = new MainPageViewModel();
    public MainView()
	{
		InitializeComponent();
        BindingContext = _mainViewModel;

    }

    private void checkBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        _mainViewModel.Update_Info();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        var taskView = new NewTaskView()
        {
            BindingContext = new AddTaskViewModel
            {
                Tasks = _mainViewModel.Tasks,
                Categories = _mainViewModel.Categories
            }
        };

        if (taskView != null )
        {
            Navigation.PushAsync(taskView);
        }
        else
        {
            DisplayAlert("Error!", "Loading this page", "Ok");
        }

        
    }
}