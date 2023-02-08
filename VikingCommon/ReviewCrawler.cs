using HtmlAgilityPack;
using VikingCommon.Models;

namespace VikingCommon;

public class ReviewCrawler
{
    public string BaseUrl { get; set; } = @"https://apnews.com";
    public async Task<List<Review>> StartCrawlerAsync()
    {
        var url = $@"{BaseUrl}/hub/film-reviews?utm_source=apnewsnav&utm_medium=sections";
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
            review.Url = BaseUrl + div.Descendants("a").FirstOrDefault()?.ChildAttributes("href").FirstOrDefault()?.Value;
            
            var spans = div.Descendants("span").ToList();
            var dat = spans.Where(p_node => p_node.GetAttributeValue("data-key", "")
                .Equals("timestamp")).ToList();
            review.Date = DateTime.TryParse(dat.FirstOrDefault()?.InnerText, out DateTime thisDateTime) ? thisDateTime : DateTime.UtcNow;
            review.Blurb = div.Descendants("p").FirstOrDefault()?.InnerText;
            review.Author = spans.FirstOrDefault()?.InnerText;
            reviews.Add(review);
        }
        return reviews;
    }
}