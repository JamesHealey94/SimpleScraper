using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleScraper
{
    public class SimpleScraper
    {
        public static async Task<Dictionary<string, string[]>> Scrape(string input)
        {
            var urlStandardiser = new UrlStandardiser(input);
            var linkExtractor = new SameDomainLinkExtractor(urlStandardiser);

            var scrape = new Dictionary<string, string[]>();

            var url = urlStandardiser.Standardise(input);

            var html = await HtmlDownloader.GetHtml(url);
            var links = linkExtractor.Extract(html.Text);
            scrape[url] = links;

            foreach(var link in links)
            {
                if (scrape.ContainsKey(link))
                {
                    Console.WriteLine("URL already scraped: " + link);
                }
                else
                {
                    var html2 = await HtmlDownloader.GetHtml(link);
                    if (html2 == null)
                    {
                        Console.WriteLine("Not valid HTML: " + link);
                        scrape[link] = Array.Empty<string>();
                    }
                    else
                    {
                        var links2 = linkExtractor.Extract(html2.Text);
                        scrape[link] = links2;
                        Console.WriteLine("scrape[" + link + "]: " + string.Join(',', links2));
                    }
                }
            }

            return scrape;
        }
    }
}
