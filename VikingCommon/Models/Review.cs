namespace VikingCommon.Models;

public class Review
{
    public string? Title { get; set; }
    public string? Url { get; set; }
    public DateTime Date { get; set; }
    public string? Blurb { get; set; }
    public string? Author { get; set; }

    public void OpenInBrowser()
    {
        if (Url != null) Browser.OpenBrowser(Url);
    }
}