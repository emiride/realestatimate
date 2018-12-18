using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;
using RealEstatimate.Models;

namespace Scraper
{
    public class Scraper
    {
        private readonly HtmlWeb _web;
        public HtmlDocument HtmlDocument { get; set; }
        public Scraper()
        {
            _web = new HtmlWeb();
            HtmlDocument = new HtmlDocument();
        }

        public Scraper(Uri uri)
        {
            _web = new HtmlWeb();
            HtmlDocument = _web.Load(uri);
        }

        public virtual RealEstateProperty Scrape()
        {
            return new RealEstateProperty();
        }
    }
}
