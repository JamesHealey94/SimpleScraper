using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleScraper
{
    public class LinkExtractor : ILinkExtractor
    {
        public virtual string[] Extract(string html)
        {
            var doc = new HtmlDocument();
            try
            {
                doc.LoadHtml(html);
                var tags = ExtractAllAHrefTags(doc);
                return tags.Distinct().OrderBy(x => x).ToArray();
            }
            catch
            {
                Console.WriteLine("Page is not valid HTML: " + html);
                return null;
            }
        }

        /// <summary>
        /// Extract all anchor tags using HtmlAgilityPack
        /// https://articles.runtings.co.uk/2009/11/easily-extracting-links-from-snippet-of.html
        /// </summary>
        private static List<string> ExtractAllAHrefTags(HtmlDocument htmlSnippet)
        {
            var hrefTags = new List<string>();

            foreach (var link in htmlSnippet.DocumentNode.SelectNodes("//a[@href]"))
            {
                var att = link.Attributes["href"];
                hrefTags.Add(att.Value);
            }

            return hrefTags;
        }
    }
}
