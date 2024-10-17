using Microsoft.Maui.Controls;
using TaskFlow.Models;
using TaskFlow.Services;

namespace TaskFlow.Views;

public partial class RegisterPage : ContentPage
{
	private readonly DatabaseService _db;
	public RegisterPage()
	{
		InitializeComponent();
		_db = new DatabaseService();
	}

	private async void OnRegisterClicked(object sender, EventArgs e)
	{
		var username = UsernameEntry.Text;
		var password = PasswordEntry.Text;

		if(string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
		{
            await DisplayAlert("Error", "Please enter both username and password", "OK");
            return;
        }

		var existingUser = _db.Users.FirstOrDefault(u => u.Username == username);
		if(existingUser != null)
		{
            await DisplayAlert("Error", "Username already exists", "OK");
            return;
        }

		var newUser = new User
		{
			Username = username,
			Password = password
		};

		_db.Users.Add(newUser);
		_db.SaveChanges();

        await DisplayAlert("Success", "User registered successfully", "OK");
        await Navigation.PopAsync();
    }
}