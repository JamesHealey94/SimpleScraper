using System;
using System.Collections.Generic;

namespace SimpleScraper
{
    public class ConsoleScrapeDisplayer : IScraperDisplayer
    {
        public void Scrape(Dictionary<string, string[]> scrape)
        {
            foreach(var key in scrape.Keys)
            {
                Console.WriteLine(key);

                foreach (var val in scrape[key])
                {
                    Console.WriteLine(" - " + val);
                }
            }
        }
    }
}
