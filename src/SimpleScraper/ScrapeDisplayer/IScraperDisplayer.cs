using System.Collections.Generic;

namespace SimpleScraper
{
    public interface IScraperDisplayer
    {
        public void Scrape(Dictionary<string, string[]> scrape);
    }
}
