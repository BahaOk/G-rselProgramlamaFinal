using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppFinal
{
    class HaberlerServis
    {
        public static async Task<Root> GetNews(Kategori category)
        {
            string apiUrl = "https://api.rss2json.com/v1/api.json?rss_url=" + category.Link;

            try
            {
                string jsonResponse = await GetJsonAsync(apiUrl);

                if (!string.IsNullOrEmpty(jsonResponse))
                {
                    Console.WriteLine(jsonResponse);
                    Root root = JsonConvert.DeserializeObject<Root>(jsonResponse);

                    return root;


                }
                else
                {
                    Console.WriteLine("Empty or null JSON response.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

        static async Task<string> GetJsonAsync(string apiUrl)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    throw new HttpRequestException($"Failed to fetch data. Status code: {response.StatusCode}");
                }
            }
        }
    }
}
