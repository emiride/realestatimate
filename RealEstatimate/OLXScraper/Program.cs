using System;
using System.Net.Http;
using OLXScraper.Enums;

namespace OLXScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            OlxLinkCollector linkCollector = new OlxLinkCollector();
            linkCollector.CollectLinks(Category.Flat);
        }
    }
}
