using System;
using System.Collections.Generic;

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
            var retVals = new List<string>();
            foreach (var link in allLinks)
            {
                if (IsRelative(link))
                {
                    retVals.Add(Domain + link);
                }
                else if (SameDomain(link))
                {
                    retVals.Add(link);
                }
            }
            return retVals.ToArray();
        }

        private static bool IsRelative(string url)
        {
            return url.StartsWith("/");
        }

        private bool SameDomain(string url)
        {
            try
            {
                return Domain == new Uri(url).Host;
            }
            catch
            {
                Console.WriteLine("Invalid URL - Couldn't create URI: " + url);
                return false;
            }
        }
    }
}
