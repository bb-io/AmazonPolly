using Blackbird.Applications.Sdk.Utils.Sdk.DataSourceHandlers;

namespace Apps.AmazonPolly.DataSourceHandlers.EnumHandlers;

public class EngineDataHandler : EnumDataHandler
{
    protected override Dictionary<string, string> EnumValues => new()
    {
        { "neural", "Neural" },
        { "standard", "Standard" },
    };
}