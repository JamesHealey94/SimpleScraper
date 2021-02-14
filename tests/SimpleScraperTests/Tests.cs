using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleScraper;
using System.Collections.Generic;
using System.IO;

namespace SimpleScraperTests
{
    [TestClass]
    public class Tests
    {
        private static readonly string Url = "https://monzo.com";
        private static readonly string HomepageHtml = File.ReadAllText("resources/Monzo – Banking made easy.html");
        private static readonly string[] MonzoLinks = File.ReadAllLines("resources/same-domain-links.txt");

        [TestMethod]
        public void Get_Html_From_Url()
        {
            var expected = HomepageHtml;
            var result = new HtmlDownloader().GetHtml(Url);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Get_Same_Domain_Links_From_Single_Page()
        {
            var expected = MonzoLinks;
            var result = new SameDomainLinkExtractor(Url).Extract(HomepageHtml);
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Scrape_Url()
        {
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
            var result = new SimpleScraper.SimpleScraper().Scrape(Url);
            CollectionAssert.AreEqual(expected, result);
        }
    }
}
