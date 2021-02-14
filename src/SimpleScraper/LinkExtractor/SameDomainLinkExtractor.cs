using System;
using System.Linq;

namespace SimpleScraper
{
    public class SameDomainLinkExtractor : LinkExtractor
    {
        private readonly string Domain;

        public SameDomainLinkExtractor(string url)
        {
            Domain = new Uri(url).Host;
        }

        public override string[] Extract(string html)
        {
            var allLinks = base.Extract(html);
            return allLinks.Where(SameDomain).ToArray();
        }

        private bool SameDomain(string url)
        {
            return Domain == new Uri(url).Host;
        }
    }
}
