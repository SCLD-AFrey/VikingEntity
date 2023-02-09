using System.Text.Json;
using System.Text.Json.Serialization;
using HtmlAgilityPack;
using VikingCommon.Models;

namespace VikingCommon;

public class ReviewCrawler
{
    public async Task StartCrawlerAsync()
    {
        var url = @"https://apnews.com/hub/film-reviews?utm_source=apnewsnav&utm_medium=sections";
        var httpClient = new HttpClient();
        var html = await httpClient.GetStringAsync(url);
        var htmlDocument = new HtmlDocument();
        htmlDocument.LoadHtml(html);
        var divs = htmlDocument.DocumentNode.Descendants("div")
            .Where(p_node => p_node.GetAttributeValue("data-key", "")
                .Equals("feed-card-wire-story-with-image")).ToList();

        var reviews = new List<Review>();
        foreach (var div in divs)
        {
            var review = new Review();
            review.Title = div.Descendants("h2").FirstOrDefault()?.InnerText;
            review.Url = url + div.Descendants("a").FirstOrDefault()?.ChildAttributes("href").FirstOrDefault()?.Value;
            
            var spans = div.Descendants("span").ToList();
            var dat = spans.Where(p_node => p_node.GetAttributeValue("data-key", "")
                .Equals("timestamp")).ToList();
            review.Date = DateTime.TryParse(dat.FirstOrDefault()?.InnerText, out DateTime thisDateTime) ? thisDateTime : DateTime.UtcNow;
            review.Blurb = div.Descendants("p").FirstOrDefault()?.InnerText;
            review.Author = spans.FirstOrDefault()?.InnerText;
            reviews.Add(review);
        }

        JsonSerializerOptions options = 
            new() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };
        var jsonString = JsonSerializer.Serialize(reviews, options);
        await File.WriteAllTextAsync(AppFiles._appFileReviews, jsonString);
    }
    
    public List<Review> Load()
    {
        if (File.Exists(AppFiles._appFileReviews))
        {
            var jsonString = File.ReadAllText(AppFiles._appFileReviews);
            if (!Helpers.JsonHelp.IsValidJson(jsonString))
            {
                return new List<Review>();
            }
            else
                return JsonSerializer.Deserialize<List<Review>>(jsonString);
        }
        return new List<Review>();
    }
}