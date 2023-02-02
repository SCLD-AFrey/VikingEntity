using System.Xml.Linq;
using VikingCommon.Models;
using HtmlAgilityPack;
using System;
using System.IO;

namespace VikingCommon;

public static class RssReader
{
    public static List<RssItem> GetRssItems(string p_url)
    {
        var rssItems = new List<RssItem>();
        try
        {
            var rss = XDocument.Load(p_url);
            var items = rss.Descendants("item");
            foreach (var item in items)
            {
                var rssItem = new RssItem
                {
                    Title = item.Element("title")?.Value ?? "",
                    Link = item.Element("link")?.Value ?? "",
                    ContentHtml = item.Element("description")?.Value ?? "",
                    ContentPlain = (item.Element("description")?.Value ?? "")
                        .Replace("<br />", "\r\n")
                        .Replace("<p>","")
                        .Replace("</p>", "\r\n"),
                    PubDate = item.Element("pubDate")?.Value ?? ""
                };
                rssItems.Add(rssItem);
            }
        }
        catch (Exception)
        {
            // ignored
        }
        return rssItems;
    }
}