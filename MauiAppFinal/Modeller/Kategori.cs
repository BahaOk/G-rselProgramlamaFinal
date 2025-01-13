using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppFinal
{
    public class Kategori
    {

        public string Tittle { get; set; }
        public string Link { get; set; }

        public static List<Kategori> CategoryList = new List<Kategori>()
        {
            new Kategori() { Tittle = "Manşet", Link = "https://www.trthaber.com/manset_articles.rss"},
            new Kategori() { Tittle = "Son Dakika", Link = "https://www.trthaber.com/sondakika_articles.rss"},
            new Kategori() { Tittle = "Gündem", Link = "https://www.trthaber.com/gundem_articles.rss"},
            new Kategori() { Tittle = "Ekonomi", Link = "https://www.trthaber.com/ekonomi_articles.rss"},
            new Kategori() { Tittle = "Spor", Link = "https://www.trthaber.com/spor_articles.rss"},
            new Kategori() { Tittle = "Bilim Teknoloji", Link = "https://www.trthaber.com/bilim_teknoloji_articles.rss"},
            new Kategori() { Tittle = "Güncel", Link = "https://www.trthaber.com/guncel_articles.rss"},
            new Kategori() { Tittle = "Eğitim", Link = "https://www.trthaber.com/egitim_articles.rss"},
        };
    }
}
