using System.Collections.Generic;

namespace SimpleScraper
{
    public interface IScrapeDisplayer
    {
        public void Display(Dictionary<string, string[]> scrape);
    }
}
