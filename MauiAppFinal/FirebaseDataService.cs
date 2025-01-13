using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MauiAppFinal;

public class FirebaseDatabaseService
{
    private readonly string databaseUrl = "https://your-database-name.firebaseio.com/"; // Realtime Database URL'inizi buraya ekleyin
    private readonly HttpClient _httpClient;

    public FirebaseDatabaseService()
    {
        _httpClient = new HttpClient();
    }

    
    public async Task AddTaskAsync(string userId, TaskItem task)
    {
        var url = $"{databaseUrl}users/{userId}/tasks.json?auth={await GetIdTokenAsync()}";
        var content = new StringContent(JsonConvert.SerializeObject(task), Encoding.UTF8, "application/json");
        await _httpClient.PostAsync(url, content);
    }

    public async Task DeleteTaskAsync(string userId, string taskId)
    {
        var url = $"{databaseUrl}users/{userId}/tasks/{taskId}.json?auth={await GetIdTokenAsync()}";
        await _httpClient.DeleteAsync(url);
    }

    public async Task UpdateTaskAsync(string userId, string taskId, TaskItem task)
    {
        var url = $"{databaseUrl}users/{userId}/tasks/{taskId}.json?auth={await GetIdTokenAsync()}";
        var content = new StringContent(JsonConvert.SerializeObject(task), Encoding.UTF8, "application/json");
        await _httpClient.PutAsync(url, content);
    }

    public async Task<List<TaskItem>> GetTasksAsync(string userId)
    {
        var url = $"{databaseUrl}users/{userId}/tasks.json?auth={await GetIdTokenAsync()}";
        var response = await _httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            var tasks = JsonConvert.DeserializeObject<Dictionary<string, TaskItem>>(result);
            return tasks?.Values.ToList() ?? new List<TaskItem>();
        }
        return new List<TaskItem>();
    }

    public async Task<Dictionary<string, TaskItem>> GetTasksWithIdsAsync(string userId)
    {
        var url = $"{databaseUrl}users/{userId}/tasks.json?auth={await GetIdTokenAsync()}";
        var response = await _httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Dictionary<string, TaskItem>>(result) ?? new Dictionary<string, TaskItem>();
        }
        return new Dictionary<string, TaskItem>();
    }

    private async Task<string> GetIdTokenAsync()
    {
        return "your-id-token";
    }
}