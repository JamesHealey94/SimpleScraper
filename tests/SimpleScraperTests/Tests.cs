using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleScraper;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SimpleScraperTests
{
    [TestClass]
    public class Tests
    {
        private static readonly string Url = "https://www.monzo.com";
        private static readonly string AgilityPackHtml = File.ReadAllText("resources/html-agility-pack-download.cshtml");
        private static readonly string[] MonzoLinks = File.ReadAllLines("resources/same-domain-links.txt");

        [TestMethod]
        public void Standardise_Url()
        {
            var expected = "https://www.monzo.com";
            var result = new UrlStandardiser(Url).Standardise(Url);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Standardise_Url_2()
        {
            var expected = "https://www.monzo.com/test";
            var result = new UrlStandardiser(Url).Standardise("https://www.monzo.com/test/");
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Standardise_Url_3()
        {
            var expected = "https://www.monzo.com/test";
            var result = new UrlStandardiser(Url).Standardise("https://www.monzo.com/test?hello=world");
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public async Task Get_Html_From_UrlAsync()
        {
            var expected = AgilityPackHtml;
            var result = (await HtmlDownloader.GetHtml(Url)).Text;
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Get_Same_Domain_Links_From_Single_Page()
        {
            var expected = MonzoLinks;
            var result = new SameDomainLinkExtractor(new UrlStandardiser(Url)).Extract(AgilityPackHtml);
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public async Task Scrape_Url()
        {
            var expected = new Dictionary<string, string[]>
            {
                {
                    "wwww.monzo.com", MonzoLinks
                },
                {
                    "www.monzo.com/about", new string[] { "TODO" }
                },
                {
                    "www.monzo.com/blog", new string[] { "TODO" }
                },
                //.... TODO
            };
            var result = await SimpleScraper.SimpleScraper.Scrape(Url);
            CollectionAssert.AreEqual(expected, result);
        }
    }
}
