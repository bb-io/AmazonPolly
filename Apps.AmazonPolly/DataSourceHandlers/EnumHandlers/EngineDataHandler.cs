using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.AmazonPolly.DataSourceHandlers.EnumHandlers;

public class EngineDataHandler : IStaticDataSourceHandler
{
    public Dictionary<string, string> GetData() => new()
    {
        { "neural", "Neural" },
        { "standard", "Standard" },
    };
}