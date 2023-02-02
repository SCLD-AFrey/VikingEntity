using HtmlAgilityPack;
using System.Net.Http;
using VikingCrawler.Models;

namespace VikingCrawler;

static class Program
{
    static async Task Main(string[] p_args)
    {
        var revs = await StartCrawlerAsync();
        Console.ReadKey();
    }

    private static async Task<List<Review>> StartCrawlerAsync()
    {
        var url = @"https://apnews.com/hub/film-reviews?utm_source=apnewsnav&utm_medium=sections";
        var httpClient = new HttpClient();
        var html = await httpClient.GetStringAsync(url);
        var htmlDocument = new HtmlDocument();
        htmlDocument.LoadHtml(html);
        var divs = htmlDocument.DocumentNode.Descendants("div")
            .Where(p_node => p_node.GetAttributeValue("data-key", "")
            .Equals("feed-card-wire-story-with-image")).ToList();

        List<Review> reviews = new List<Review>();
        foreach (var div in divs)
        {
            var review = new Review();
            review.Title = div.Descendants("h2").FirstOrDefault()?.InnerText;
            review.Url = div.Descendants("a").FirstOrDefault()?.ChildAttributes("href").FirstOrDefault()?.Value;
            
            var spans = div.Descendants("span").ToList();
            var dat = spans.Where(p_node => p_node.GetAttributeValue("data-key", "")
                .Equals("timestamp")).ToList();
            review.Date = DateTime.TryParse(dat.FirstOrDefault()?.InnerText, out DateTime thisDateTime) ? thisDateTime : DateTime.UtcNow;
            review.Blurb = div.Descendants("p").FirstOrDefault()?.InnerText;
            
            reviews.Add(review);
        }
        return reviews;
    }


}