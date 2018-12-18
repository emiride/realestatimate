using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using OLXScraper.Enums;

namespace OLXScraper
{
    class OlxLinkCollector
    {
        public List<Uri> HrefList { get; set; }
        public int NumberOfPages { get; set; }
        public HtmlDocument HtmlDocument { get; set; }

        public OlxLinkCollector()
        {
            HrefList = new List<Uri>();
            HtmlWeb web = new HtmlWeb();
            HtmlDocument = web.Load("https://www.olx.ba/pretraga?kategorija=23&stranica=1");
            NumberOfPages = GetNumberOfPages();
        }

        public void CollectLinks(Category category)
        {
            for (int i = 1; i < NumberOfPages; i++)
            {
                CollectLinksOnPage(i, category);
            }
        }
        private int GetNumberOfPages()
        {
            var numberOfResults = HtmlDocument.DocumentNode.SelectNodes(".//div[@class='brojrezultata']/span").First()
                .InnerText.Replace(",", "").Replace(".","");
            return int.Parse(numberOfResults) / 30 + 1;
        }

        private void CollectLinksOnPage(int page, Category category)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument document = web.Load($"https://www.olx.ba/pretraga?kategorija={(int)category}&stranica={page}");

            var urls = document.DocumentNode.SelectNodes(".//div[@class='naslov']/a");
            foreach (var url in urls)
            {
                HrefList.Add(new Uri(url.Attributes["href"].Value));
            }
        }
        
    }
}
