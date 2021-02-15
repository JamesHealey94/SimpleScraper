# SimpleScraper
A simple web scraper. You give it a domain, it'll give you the content.

- Given a starting URL, the crawler visits each URL it finds on the same domain.
- It prints each URL visited, and a list of links found on that page.
- The crawler is limited to one subdomain: if you start with *https://monzo.com/*, it would crawl all pages within monzo.com, but not follow external links, for example to facebook.com or community.monzo.com.

## Build and Run
- `dotnet publish src/SimpleScraper.Runner -o output`
- `docker build -t simple-scraper .`
- `docker run -t simple-scraper https://monzo.com/`

## Example output
See example-output.txt

## Run Tests
- `dotnet test tests\SimpleScraperTests\SimpleScraperTests.csproj`

## Dependencies
- [HTML Agility Pack](https://html-agility-pack.net/) is used for HTML download and parsing

## Future improvements
- Improve test coverage of unhappy paths (user input, web calls, html content)
- Optionally take [robots.txt](https://monzo.com/robots.txt) into account
- Optional delay between HTML downloads
- Optional "depth"
- Option for breadth-first or depth-first search
- Parallelize - perhaps using concurrent dictionary
- Improve logging - Use log levels to hide debug info
- Add retry logic to web calls