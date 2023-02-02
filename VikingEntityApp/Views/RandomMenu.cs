using System.Diagnostics;
using System.Text.Json;
using System.Xml;
using VikingCommon;
using VikingCommon.Models;

namespace VikingEntityConsole.Views;

public class RandomMenu : AbstractMenu
{
    public RandomMenu() : base("Random Menu")
    {
    }

    protected override void Init()
    {
        AddMenuItem(new MenuItem(1, "Crawl AP Movie Reviews", CrawlApMovieReviews));
        AddMenuItem(new MenuItem(2, "Open Movie Review in browser", OpenApMovieReviews));
        AddMenuItem(new MenuItem(3, "View AP Movie Reviews", ViewApMovieReviews));
        
        
        AddMenuItem(new MenuItem(4, "Read RSS Feed", ReadRssFeed));
        
        AddMenuItem(new MenuItem(5, "Return").SetAsExitOption());
    }

    private void ReadRssFeed()
    {
        string url = "https://www.state.gov/rss-feed/arms-control-and-international-security/feed/";
        var feed = RssReader.GetRssItems(url);
        foreach (var item in feed)
        {   
            Console.WriteLine($"{DateTime.Parse(item.PubDate).ToString("MM-dd")} {item.Title}");
        }
    }

    private void OpenApMovieReviews()
    {
        var reviews = JsonSerializer.Deserialize<List<Review>>(JsonTools.LoadOptions(AppFiles._appFileReviews));
        string indexStr = "";
        int index = 0;
        while (string.IsNullOrEmpty(indexStr) || index == 0)
        {
            Console.Write("Enter review number: ('exit' to exit) ");
            indexStr = Console.ReadLine();
            if (indexStr.ToLower().Equals("exit"))
            {
                return;
            }
            index = int.TryParse(indexStr, out var badIndex) ? badIndex : 0;
        }
        reviews[index - 1].OpenInBrowser();
    }

    private static async void CrawlApMovieReviews()
    {
        ReviewCrawler rc = new ReviewCrawler();
        var reviews = await rc.StartCrawlerAsync();
        JsonTools.SaveOptions(AppFiles._appFileReviews, JsonSerializer.Serialize(reviews));
    }

    private static void ViewApMovieReviews()
    {
        var reviews = JsonSerializer.Deserialize<List<Review>>(JsonTools.LoadOptions(AppFiles._appFileReviews));
        int i = 0;
        foreach (var review in reviews.OrderBy(r => r.Title))
        {
            i++;
            var title = review.Title != null && review.Title.Length > 60 ? review.Title.Substring(0, 60) : review.Title;
            var blurb = review.Blurb != null && review.Blurb.Length > 185 ? review.Blurb.Substring(0, 185) + "..." : review.Blurb;
            Messages.Results($"{i, 3}." 
                + $" {title, -100}" 
                + $" {review.Date, -15}"
                + $" {review.Author, -15}", (i%2==0));
            Messages.Results($"{"", 3}  {blurb}", (i%2==0));
        }

    }

}
