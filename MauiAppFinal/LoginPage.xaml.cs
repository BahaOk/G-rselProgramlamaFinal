namespace MauiAppFinal{

public partial class LoginPage : ContentPage
{
    private FirebaseService _firebaseService;
    public LoginPage()
    {
        InitializeComponent();
        _firebaseService = new FirebaseService();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        var email = emailEntry.Text;
        var password = passwordEntry.Text;

        var result = await _firebaseService.LoginUserAsync(email, password);

        if (result != null)
        {
            await DisplayAlert("Success", "Login Successful", "OK");
            Application.Current.MainPage = new AppShell();
        }
        else
        {
            await DisplayAlert("Error", "Login Failed", "OK");
        }
    }

   private async void OnRegisterClicked(object sender, EventArgs e)
{
    var email = emailEntry.Text;
    var password = passwordEntry.Text;

    var result = await _firebaseService.RegisterUserAsync(email, password);

    if (!string.IsNullOrEmpty(result) && !result.StartsWith("Error"))
    {
        await DisplayAlert("Success", "Registration Successful", "OK");
        await Navigation.PushAsync(new LoginPage());
    }
    else
    {
        await DisplayAlert("Error", result ?? "An error occurred", "OK");
    }
}

}
}