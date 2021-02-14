using HtmlAgilityPack;
using System;
using System.Collections.Generic;

namespace SimpleScraper
{
    public class LinkExtractor : ILinkExtractor
    {
        public virtual string[] Extract(string html)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Extract all anchor tags using HtmlAgilityPack
        /// https://articles.runtings.co.uk/2009/11/easily-extracting-links-from-snippet-of.html
        /// </summary>
        /// <param name="htmlSnippet"></param>
        /// <returns></returns>
        private List<string> ExtractAllAHrefTags(HtmlDocument htmlSnippet)
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
