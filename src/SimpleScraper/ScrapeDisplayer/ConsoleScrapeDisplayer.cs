using System;
using System.Collections.Generic;

namespace SimpleScraper
{
    public class ConsoleScrapeDisplayer : IScrapeDisplayer
    {
        public void Display(Dictionary<string, string[]> scrape)
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
