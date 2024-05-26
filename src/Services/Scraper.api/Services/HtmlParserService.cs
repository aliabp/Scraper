using Scraper.api.Helper;

namespace Scraper.api.Services;

public class HtmlParserService : IHtmlParserService
{
    public HtmlParserService()
    {
    }
    
    public IEnumerable<string> GetTag(string tagName, string htmlDom)
    {
        try
        {
            // find a specific tag in a html document using GetHtmlTag extension
            return htmlDom.GetHtmlTags(tagName);
        }
        catch (Exception e)
        {
            // an error occured, log the error and return an empty array of tags
            LogHelper.Instance.LogError($"Something went wrong inside GetTag method: {e.Message}");
            return new List<string>();
        }

    }

    public async Task<IEnumerable<int>> GetUrlPositionsAsync(IEnumerable<string> tags, string url)
    {
        // search through a list of tags to find tags which containing target url
        return await Task.Run(() =>
        {
            try
            {
                var positions = new List<int>();
                int index = 1;
                foreach (var tag in tags)
                {
                    // positions where target url founded, store in positions list
                    if (tag.Contains(url, StringComparison.OrdinalIgnoreCase))
                    {
                        positions.Add(index);
                    }
                    index++;
                }

                return positions;
            }
            catch (Exception e)
            {
                // an error occured, log the error and return an empty array of positions
                LogHelper.Instance.LogError($"Something went wrong inside GetUrlPositionsAsync method: {e.Message}");
                return new List<int>();
            }

        });
    }
}