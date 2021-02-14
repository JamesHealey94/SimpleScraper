namespace SimpleScraper
{
    public interface ILinkExtractor
    {
        public string[] Extract(string html);
    }
}
