
// Dovizler.cs
using System.Net.Http;
using System.Threading.Tasks;

namespace MauiAppFinal
{
    public static class Dovizler
    {
        public static async Task<string> GetAltinDovizGuncelKurlar()
        {
            HttpClient client = new HttpClient();
            string url = "https://finans.truncgil.com/today.json";
            using HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string jsondata = await response.Content.ReadAsStringAsync();
            return jsondata;
        }
    }
}