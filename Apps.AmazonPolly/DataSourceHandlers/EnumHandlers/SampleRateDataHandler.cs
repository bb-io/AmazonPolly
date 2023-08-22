using Blackbird.Applications.Sdk.Utils.Sdk.DataSourceHandlers;

namespace Apps.AmazonPolly.DataSourceHandlers.EnumHandlers;

public class SampleRateDataHandler : EnumDataHandler
{
    protected override Dictionary<string, string> EnumValues => new()
    {
        { "8000", "8000" },
        { "16000", "16000" },
        { "22050", "22050" },
        { "24000", "24000" },
    };
}