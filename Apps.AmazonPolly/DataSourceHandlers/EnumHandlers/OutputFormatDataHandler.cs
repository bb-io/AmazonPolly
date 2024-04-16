using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.AmazonPolly.DataSourceHandlers.EnumHandlers;

public class OutputFormatDataHandler : IStaticDataSourceHandler
{
    public Dictionary<string, string> GetData()
        => new()
        {
            { "json", "JSON" },
            { "mp3", "MP3" },
            { "ogg_vorbis", "Ogg Vorbis" },
            { "pcm", "PCM" },
        };
}