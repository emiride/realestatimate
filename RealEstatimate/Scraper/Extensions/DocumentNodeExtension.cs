using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;

namespace Scraper.Extensions
{
    public static class DocumentNodeExtension
    {
        public static int InnerInt(this HtmlNode node)
        {
            var innerInt = 0;
            try
            {
                innerInt = int.Parse(node.InnerText.Replace(",", "").Replace(".", "").Trim());
            }
            catch (ArgumentException)
            {
                innerInt = 0;
            }

            return innerInt;
        }
    }
}
