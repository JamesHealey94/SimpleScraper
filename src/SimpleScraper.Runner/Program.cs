using System;
using System.Threading.Tasks;

namespace SimpleScraper.Runner
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine($"Scraping '{args[0]}'...");
            var scrape = await SimpleScraper.Scrape(args[0]);
            new ConsoleScrapeDisplayer().Display(scrape);
        }
    }
}
