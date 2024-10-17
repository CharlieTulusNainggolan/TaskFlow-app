using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TaskFlow.Models;
using TaskFlow.Services;

namespace TaskFlow.Views;

public partial class ViewTaskPage : ContentPage
{
	private readonly DatabaseService _db;
	private readonly User _user;

    public ViewTaskPage(User user)
	{
		InitializeComponent();
		_db = new DatabaseService();
		_user = user;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadTasks();
    }

    private void LoadTasks()
	{
		var tasks = _db.TaskItems.Where(t =>t.UserId == _user.Id).ToList();
		TasksListView.ItemsSource = tasks;
	}

    private async void OnCreateTaskClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CreateTaskPage(_user));
    }

    private async void OnCompleteClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var task = button.BindingContext as TaskItem;


        task.IsCompleted = !task.IsCompleted;
        task.Status = task.IsCompleted ? "completed" : "uncompleted";

        _db.TaskItems.Update(task);
        _db.SaveChanges();
        LoadTasks();
    }

    private async void OnEditClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var task = button.BindingContext as TaskItem;
        if (task != null)
        {
            await Navigation.PushAsync(new EditTaskPage(task));
        }
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var task = button.BindingContext as TaskItem;

        bool confirm = await DisplayAlert("Confirm", $"Delete task '{task.Title}'?", "Yes", "No");
        if (confirm)
        {
            _db.TaskItems.Remove(task);
            _db.SaveChanges();
            LoadTasks();
        }
    }

    private void OnTaskSelected(object sender, SelectedItemChangedEventArgs e)
    {
        // Opsional Implement if diperlukan
    }
}