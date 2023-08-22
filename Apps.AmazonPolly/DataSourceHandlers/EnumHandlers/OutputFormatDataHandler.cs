using Blackbird.Applications.Sdk.Utils.Sdk.DataSourceHandlers;

namespace Apps.AmazonPolly.DataSourceHandlers.EnumHandlers;

public class OutputFormatDataHandler : EnumDataHandler
{
    protected override Dictionary<string, string> EnumValues => new()
    {
        { "json", "JSON" },
        { "mp3", "MP3" },
        { "ogg_vorbis", "Ogg Vorbis" },
        { "pcm", "PCM" },
    };
}