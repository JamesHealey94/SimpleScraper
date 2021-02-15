using HtmlAgilityPack;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleScraper
{
    public class HtmlDownloader
    {
        public static async Task<HtmlDocument> GetHtml(string url)
        {
            if (url.EndsWith(".pdf"))
            {
                Console.WriteLine("URL is a PDF: " + url);
                return null;
            }

            var doc = new HtmlWeb();
            var html = await doc.LoadFromWebAsync(url);

            if (html.ParseErrors.Any(e => e.Code != HtmlParseErrorCode.EndTagNotRequired))
            {
                return null;
            }
            else
            {
                return html;
            }
        }
    }
}
