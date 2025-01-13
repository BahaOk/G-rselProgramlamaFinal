using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace MauiAppFinal;

public partial class Kurlar : ContentPage
 {
    public Kurlar()
    {
        InitializeComponent();
    }

       private static Kurlar instance;
       public static Kurlar page
             {
             get
           {
        if (instance == null)
        instance = new Kurlar();
        return instance;
          }
  }

        protected async override void OnAppearing()
          {
        base.OnAppearing();
        await Load();
        }

       private async void OnRefreshClicked(object sender, EventArgs e)
      {
      await Load();
  }

             AltinDoviz kurlar;
            async Task Load()
                  {
           string jsondata = await GetAltinDovizGuncelKurlar();
          kurlar = JsonConvert.DeserializeObject<AltinDoviz>(jsondata);
           dovizliste.ItemsSource = new List<KurItem>()
                 {
           new KurItem()
            {
                Doviz = "Dolar",
                Alis = kurlar.USD.Alis,
                Satis = kurlar.USD.Satis,
                Fark = kurlar.USD.Degisim,
                Yon = GetImage(kurlar.USD.Degisim)
            },
            new KurItem()
            {
                Doviz = "Euro",
                Alis = kurlar.EUR.Alis,
                Satis = kurlar.EUR.Satis,
                Fark = kurlar.EUR.Degisim,
                Yon = GetImage(kurlar.EUR.Degisim)
            },
       
            new KurItem()
            {
                Doviz = "SAR",
                Alis = kurlar.SAR.Alis,
                Satis = kurlar.SAR.Satis,
                Fark = kurlar.SAR.Degisim,
                Yon = GetImage(kurlar.SAR.Degisim)
            },
            new KurItem()
            {
                Doviz = "QAR",
                Alis = kurlar.QAR.Alis,
                Satis = kurlar.QAR.Satis,
                Fark = kurlar.QAR.Degisim,
                Yon = GetImage(kurlar.QAR.Degisim)
            },
            new KurItem()
            {
                Doviz = "EGP",
                Alis = kurlar.EGP.Alis,
                Satis = kurlar.EGP.Satis,
                Fark = kurlar.EGP.Degisim,
                Yon = GetImage(kurlar.EGP.Degisim)
            },
            new KurItem()
            {
                Doviz = "JPY",
                Alis = kurlar.JPY.Alis,
                Satis = kurlar.JPY.Satis,
                Fark = kurlar.JPY.Degisim,
                Yon = GetImage(kurlar.JPY.Degisim)
            },
            new KurItem()
            {
                Doviz = "INR",
                Alis = kurlar.INR.Alis,
                Satis = kurlar.INR.Satis,
                Fark = kurlar.INR.Degisim,
                Yon = GetImage(kurlar.INR.Degisim)
            },
            new KurItem()
            {
                Doviz = "AUD",
                Alis = kurlar.AUD.Alis,
                Satis = kurlar.AUD.Satis,
                Fark = kurlar.AUD.Degisim,
                Yon = GetImage(kurlar.AUD.Degisim)
            },
            new KurItem()
            {
                Doviz = "CAD",
                Alis = kurlar.CAD.Alis,
                Satis = kurlar.CAD.Satis,
                Fark = kurlar.CAD.Degisim,
                Yon = GetImage(kurlar.CAD.Degisim)
            },
            new KurItem()
            {
                Doviz = "CHF",
                Alis = kurlar.CHF.Alis,
                Satis = kurlar.CHF.Satis,
                Fark = kurlar.CHF.Degisim,
                Yon = GetImage(kurlar.CHF.Degisim)
            },
            new KurItem()
            {
                Doviz = "SGD",
                Alis = kurlar.SGD.Alis,
                Satis = kurlar.SGD.Satis,
                Fark = kurlar.SGD.Degisim,
                Yon = GetImage(kurlar.SGD.Degisim)
            }
        };
      }

        private string GetImage(string str)
        {
        if (str.Contains('-'))
            return "down.png";
        return "up.png";
       }

     private async Task<string> GetAltinDovizGuncelKurlar()
          {
        HttpClient client = new HttpClient();
        string url = "https://finans.truncgil.com/today.json";
        using HttpResponseMessage response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        string jsondata = await response.Content.ReadAsStringAsync();
        return jsondata;
        }
   }

               public class KurItem
                  {
                public string Doviz { get; set; }
                public string Alis { get; set; }
                public string Satis { get; set; }
                public string Fark { get; set; }
                public string Yon { get; set; }
                     }

            public class AltinDoviz
              {
              public string Update_Date { get; set; }
               public Currency USD { get; set; }
               public Currency EUR { get; set; }
               public Currency SAR { get; set; }
               public Currency QAR { get; set; }
               public Currency EGP { get; set; }
               public Currency JPY { get; set; }
               public Currency INR { get; set; }
                 public Currency AUD { get; set; }
                public Currency CAD { get; set; }
              public Currency CHF { get; set; }
              public Currency SGD { get; set; }
         }

                    public class Currency
          {
              [JsonProperty("Alış")]
             public string Alis { get; set; }
               [JsonProperty("Satış")]
              public string Satis { get; set; }
              [JsonProperty("Değişim")]
               public string Degisim { get; set; }
 }