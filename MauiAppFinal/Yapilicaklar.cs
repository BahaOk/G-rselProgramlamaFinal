using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Maui.Controls;

namespace MauiAppFinal;

public partial class Yapilacaklar : ContentPage
{
    public ObservableCollection<TaskItem> Tasks { get; set; }
    private readonly FirebaseDatabaseService firebaseDatabaseService;
    private readonly string userId;

    
    public Yapilacaklar()
    {
        InitializeComponent();
        Tasks = new ObservableCollection<TaskItem>();
        TasksListView.ItemsSource = Tasks;
        firebaseDatabaseService = new FirebaseDatabaseService();
        userId = string.Empty;
    }

    public Yapilacaklar(string userId) : this() 
    {
        this.userId = userId;
        LoadTasks();
    }

    private async void LoadTasks()
    {
        if (string.IsNullOrEmpty(userId))
        {
            return;
        }
        var tasks = await firebaseDatabaseService.GetTasksAsync(userId);
        Tasks.Clear();
        foreach (var task in tasks)
        {
            Tasks.Add(task);
        }
    }
    private async void GorevEkleme(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(TaskEntry.Text))
        {
            var task = new TaskItem { Task = TaskEntry.Text, IsCompleted = false };
            Tasks.Add(task);
            await firebaseDatabaseService.AddTaskAsync(userId, task);
            TaskEntry.Text = string.Empty;
        }
    }

    private async void GorevSil(object sender, EventArgs e)
    {
        var button = sender as Button;
        var task = button?.BindingContext as TaskItem;
        if (task != null)
        {
            Tasks.Remove(task);
            var taskId = await GetTaskIdAsync(task);
            if (!string.IsNullOrEmpty(taskId))
            {
                await firebaseDatabaseService.DeleteTaskAsync(userId, taskId);
            }
        }
    }

    private async void GorevDuzenle(object sender, EventArgs e)
    {
        var button = sender as Button;
        var task = button?.BindingContext as TaskItem;
        if (task != null)
        {
            TaskEntry.Text = task.Task;
            Tasks.Remove(task);
            var taskId = await GetTaskIdAsync(task);
            if (!string.IsNullOrEmpty(taskId))
            {
                await firebaseDatabaseService.DeleteTaskAsync(userId, taskId);
            }
        }
    }

    private async Task<string> GetTaskIdAsync(TaskItem task)
    {
        var tasks = await firebaseDatabaseService.GetTasksWithIdsAsync(userId);
        var taskId = tasks.FirstOrDefault(x => x.Value.Task == task.Task).Key;
        return taskId;
    }
}

public class TaskItem
{
    public string Task { get; set; }
    public bool IsCompleted { get; set; }
}