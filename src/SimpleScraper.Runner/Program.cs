using System;
using System.Threading.Tasks;

namespace SimpleScraper.Runner
{
    class Program
    {
        static async Task Main(string[] args)
        {
            if (args.Length == 1)
            {
                Console.WriteLine($"Scraping '{args[0]}'...");
                var scrape = await SimpleScraper.Scrape(args[0]);
                new ConsoleScrapeDisplayer().Display(scrape);
            }
            else
            {
                Console.WriteLine("Requires URL as argument");
            }
        }
    }
}
