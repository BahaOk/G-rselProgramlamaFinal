using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class FirebaseService
{
    private readonly string firebaseApiKey = "AIzaSyDurQVhXQLK2cMKjEE3EiN5-ML8zKSyBi8"; 
    private readonly string authUrl = "https://identitytoolkit.googleapis.com/v1/accounts:signUp?key=";

    private readonly HttpClient _httpClient;

    public FirebaseService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<string> RegisterUserAsync(string email, string password)
    {
        var requestBody = new
        {
            email,
            password,
            returnSecureToken = true
        };

        var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(authUrl + firebaseApiKey, content);

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<FirebaseResponse>(result);
            return responseObject?.IdToken; 
        }
        else
        {
            var error = await response.Content.ReadAsStringAsync();
            var errorResponse = JsonConvert.DeserializeObject<FirebaseErrorResponse>(error);
            return $"Error: {errorResponse?.Error?.Message}";
        }
    }

    public async Task<string> LoginUserAsync(string email, string password)
    {
        var loginUrl = "https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key=" + firebaseApiKey;

        var requestBody = new
        {
            email,
            password,
            returnSecureToken = true
        };

        var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(loginUrl, content);

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<FirebaseResponse>(result);
            return responseObject?.IdToken; 
        }
        else
        {
            var error = await response.Content.ReadAsStringAsync();
            var errorResponse = JsonConvert.DeserializeObject<FirebaseErrorResponse>(error);
            return $"Error: {errorResponse?.Error?.Message}";
        }
    }
}

public class FirebaseResponse
{
    public string IdToken { get; set; }
    public string RefreshToken { get; set; }
    public string LocalId { get; set; }
    public string Email { get; set; }
}

public class FirebaseErrorResponse
{
    public FirebaseError Error { get; set; }
}

public class FirebaseError
{
    public string Message { get; set; }
}
