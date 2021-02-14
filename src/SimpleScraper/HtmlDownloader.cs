using HtmlAgilityPack;
using System.Threading.Tasks;

namespace SimpleScraper
{
    public class HtmlDownloader
    {
        public static async Task<HtmlDocument> GetHtml(string url)
        {
            var doc = new HtmlWeb();
            return await doc.LoadFromWebAsync(url);
        }
    }
}
