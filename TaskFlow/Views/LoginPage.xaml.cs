using Microsoft.Maui.Controls;
using TaskFlow.Services;

namespace TaskFlow.Views;

public partial class LoginPage : ContentPage
{
	private readonly DatabaseService _db;
	public LoginPage()
	{
		InitializeComponent();
		_db = new DatabaseService();
	}

	private async void OnLoginClicked(object sender, EventArgs e)
	{
		var username = UsernameEntry.Text;
		var password = PasswordEntry.Text;

		var user = _db.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
		if (user != null)
		{
			await Navigation.PushAsync(new ViewTaskPage(user));
		}
		else
		{
			await DisplayAlert("Error", "Invalid Credentials", "OK");
		}
	}

	private async void OnRegisterClicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new RegisterPage());
	}
}