using TaskFlow.Views;

namespace TaskFlow
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage());

            //MainPage = new AppShell();
        }
    }
}
