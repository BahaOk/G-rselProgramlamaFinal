using System.Collections.ObjectModel;

namespace MauiAppFinal;

public partial class HavaDurumu : ContentPage
{public ObservableCollection<SehirHavaDurumu> Sehirler { get; set; } = new();

public HavaDurumu()
{
    InitializeComponent();
    ImageCollection.ItemsSource = Sehirler;
    
}

    private async void OnButtonClick(object sender, EventArgs e)
{
    string? sehir = await DisplayPromptAsync("Şehir", "Şehir ismi:", "OK", "Cancel");

    if (!string.IsNullOrWhiteSpace(sehir))
    {
        sehir = SehirIsimGuncelle(sehir);
        Sehirler.Add(new SehirHavaDurumu { Name = sehir });
        await DisplayAlert("Şehir Eklendi", $"{sehir} şehri eklendi.", "Tamam");
    }
    else
    {
        await DisplayAlert("Hata", "Geçerli bir şehir adı giriniz.", "Tamam");
    }
}


    private string SehirIsimGuncelle(string sehir)
    {
        sehir = sehir.ToUpper(System.Globalization.CultureInfo.CurrentCulture);
        sehir = sehir.Replace('Ç', 'C').Replace('Ğ', 'G').Replace('İ', 'I')
                     .Replace('Ö', 'O').Replace('Ü', 'U').Replace('Ş', 'S');
        return sehir;
    }

    private async void OnRefreshButtonClick(object sender, EventArgs e)
    {
        foreach (var sehir in Sehirler)
        {
            Console.WriteLine($"Yenileniyor: {sehir.Source}");
        }
        await DisplayAlert("Yenile", "Hava durumu verileri yenilendi.", "Tamam");
    }

    private async void ImageCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is SehirHavaDurumu selectedSehir)
        {
            await DisplayAlert("Seçilen Şehir", $"Seçilen Şehir: {selectedSehir.Name}", "Tamam");
        }

        ((CollectionView)sender).SelectedItem = null;
    }
}
