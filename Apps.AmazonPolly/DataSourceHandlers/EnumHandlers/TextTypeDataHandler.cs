using Blackbird.Applications.Sdk.Utils.Sdk.DataSourceHandlers;

namespace Apps.AmazonPolly.DataSourceHandlers.EnumHandlers;

public class TextTypeDataHandler : EnumDataHandler
{
    protected override Dictionary<string, string> EnumValues => new()
    {
        { "text", "Text" },
        { "ssml", "SSML" }
    };
}