using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleScraper
{
    public class SameDomainLinkExtractor : LinkExtractor
    {
        private readonly UrlStandardiser UrlStandardiser;

        public SameDomainLinkExtractor(UrlStandardiser urlStandardiser)
        {
            UrlStandardiser = urlStandardiser;
        }

        public override string[] Extract(string html)
        {
            var allLinks = base.Extract(html);
            var retVals = new List<string>();
            foreach (var link in allLinks)
            {
                if (UrlStandardiser.IsRelative(link) || UrlStandardiser.SameDomain(link))
                {
                    retVals.Add(link);
                }
                else
                {
                    Console.WriteLine("Different domain: " + link);
                }
            }
            return retVals.Select(UrlStandardiser.Standardise).Distinct().ToArray();
        }
    }
}
