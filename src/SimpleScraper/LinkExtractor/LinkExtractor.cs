using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;

namespace SimpleScraper
{
    public class LinkExtractor : ILinkExtractor
    {
        public virtual string[] Extract(string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            var tags = ExtractAllAHrefTags(doc);
            return tags.Distinct().OrderBy(x => x).ToArray();
        }

        /// <summary>
        /// Extract all anchor tags using HtmlAgilityPack
        /// https://articles.runtings.co.uk/2009/11/easily-extracting-links-from-snippet-of.html
        /// </summary>
        /// <param name="htmlSnippet"></param>
        /// <returns></returns>
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
