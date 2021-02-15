using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleScraper
{
    public class SimpleScraper
    {
        public static async Task<Dictionary<string, string[]>> Scrape(string input)
        {
            var UrlStandardiser = new UrlStandardiser(input);
            var url = UrlStandardiser.Standardise(input);

            var scrape = new Dictionary<string, string[]>();

            var html = await HtmlDownloader.GetHtml(url);

            var links = new SameDomainLinkExtractor(UrlStandardiser).Extract(html.Text);

            scrape[url] = links;

            return scrape;
        }
    }
}
