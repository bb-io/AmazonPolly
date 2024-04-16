using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.AmazonPolly.DataSourceHandlers.EnumHandlers;

public class TextTypeDataHandler : IStaticDataSourceHandler
{
    public Dictionary<string, string> GetData()
        => new()
        {
            { "text", "Text" },
            { "ssml", "SSML" }
        };
}