using System;

namespace SimpleScraper
{
    public class UrlStandardiser
    {
        public readonly string Domain;

        public UrlStandardiser(string url)
        {
            Domain = new Uri(url).Host;
        }

        public string Standardise(string input)
        {
            try
            {
                string url = "";
                if (IsRelative(input))
                {
                    url = "https://" + Domain + input;
                }
                else
                {
                    var uri = new Uri(input);
                    url = "https://" + uri.Host + uri.AbsolutePath;
                }

                if (url.EndsWith("/") || url.EndsWith("#"))
                {
                    // Remove trailing '/' or '#'
                    return url[0..^1];
                }
                else
                {
                    return url;
                }
            }
            catch
            {
                Console.WriteLine("Invalid URL - Couldn't create URI: " + input);
                return input;
            }
        }

        public bool IsRelative(string url)
        {
            return url.StartsWith("/") || url.StartsWith("#") || url.StartsWith(".");
        }

        public bool SameDomain(string input)
        {
            try
            {
                return Domain == new Uri(input).Host;
            }
            catch
            {
                Console.WriteLine("Invalid URL - Couldn't create URI: " + input);
                return false;
            }
        }
    }
}