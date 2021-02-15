using HtmlAgilityPack;
using System.Threading.Tasks;

namespace SimpleScraper
{
    public class HtmlDownloader
    {
        public static async Task<HtmlDocument> GetHtml(string url)
        {
            // TODO - deal with failures - .pdf url
            var doc = new HtmlWeb();
            return await doc.LoadFromWebAsync(url);
        }
    }
}
