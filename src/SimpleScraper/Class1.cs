using System;
using System.Collections.Generic;

namespace SimpleScraper
{
    public class HtmlDownloader
    {
        public string GetHtml(string url)
        {
            throw new NotImplementedException();
        }
    }

    public class SimpleScraper
    {
        public Dictionary<string, string[]> Scrape(string url)
        {
            throw new NotImplementedException();
        }
    }

    public interface IScraperDisplayer
    {
        public void Scrape(Dictionary<string, string[]> scrape);
    }

    public class ConsoleScrapeDisplayer : IScraperDisplayer
    {
        public void Scrape(Dictionary<string, string[]> scrape)
        {
            throw new NotImplementedException();
        }
    }
}
