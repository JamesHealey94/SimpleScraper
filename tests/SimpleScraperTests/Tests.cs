using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleScraper;
using System.Collections.Generic;
using System.IO;

namespace SimpleScraperTests
{
    [TestClass]
    public class Tests
    {
        private static readonly string HomepageHtml = File.ReadAllText("resources/Monzo – Banking made easy.html");
        private static readonly string[] AllLinks = File.ReadAllLines("resources/all-links.txt");
        private static readonly string[] MonzoLinks = File.ReadAllLines("resources/same-domain-links.txt");

        [TestMethod]
        public void Get_Html_From_Url()
        {
            var url = "https://monzo.com";
            var expected = HomepageHtml;

            var result = new HtmlDownloader().GetHtml(url);
            Assert.AreEqual(expected, result);
        }
        
        [TestMethod]
        public void Get_Links_From_Single_Page()
        {
            var expected = AllLinks;
            var result = new LinkExtractor().Extract(HomepageHtml);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Get_Same_Domain_Links_From_Single_Page()
        {
            var expected = MonzoLinks;
            var result = new SameDomainLinkExtractor().Extract(HomepageHtml);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Scrape_Url()
        {
            var url = "https://monzo.com";
            var result = new SimpleScraper.SimpleScraper().Scrape(url);
            var expected = new Dictionary<string, string[]>
            {
                {
                    "https://monzo.com", MonzoLinks
                },
                {
                    "https://monzo.com/about", new string[] { "TODO" }
                },
                {
                    "https://monzo.com/blog/", new string[] { "TODO" }
                },
                //.... TODO
            };
            Assert.AreEqual(expected, result);
        }
    }
}
