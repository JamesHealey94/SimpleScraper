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
            var result = new LinkExtractor().Extract(HomepageHtml);
            var expected = new List<string>()
            {
                "https://monzo.com",
                ""
            };
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Get_Same_Domain_Links_From_Single_Page()
        {
            var result = new SameDomainLinkExtractor().Extract(HomepageHtml);
            var expected = new List<string>()
            {
                "https://monzo.com",
                ""
            };
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Get_HTML_Links_From_Single_Page()
        {
            var result = new HtmlLinkExtractor().Extract(HomepageHtml);
            var expected = new List<string>()
            {
                "https://monzo.com",
                ""
            };
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Scrape_Site()
        {
            var url = "https://monzo.com";
            var result = new SimpleScraper.SimpleScraper().Scrape(url);
            var expected = new Dictionary<string, List<string>>
            {
                {
                    "https://monzo.com",
                    new List<string>()
                    {
                        "",
                        "",
                        ""
                    }
                }
            };
            Assert.AreEqual(expected, result);
        }
    }
}
