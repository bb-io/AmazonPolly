using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.AmazonPolly.DataSourceHandlers.EnumHandlers;

public class SampleRateDataHandler : IStaticDataSourceHandler
{
    public Dictionary<string, string> GetData()
        => new()
        {
            { "8000", "8000" },
            { "16000", "16000" },
            { "22050", "22050" },
            { "24000", "24000" },
        };
}