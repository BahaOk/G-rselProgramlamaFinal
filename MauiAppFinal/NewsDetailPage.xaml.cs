namespace MauiAppFinal;
public partial class NewsDetailPage : ContentPage
{
    public NewsDetailPage(string url)
    {
        InitializeComponent();

        webView.Source = url;

    }

    private void ShareNews_Clicked(object sender, EventArgs e)
    {

    }
}
