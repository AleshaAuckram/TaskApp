	using TaskApp.Pattern.Views;
using TaskApp.Pattern.ViewModels;
namespace TaskApp;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
		MainPage = new NavigationPage(new MainView());
		BindingContext = new MainPageViewModel();
    }
}
