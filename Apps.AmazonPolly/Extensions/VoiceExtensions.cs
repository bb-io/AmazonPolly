using Amazon.Polly.Model;
using Blackbird.Applications.Sdk.Utils.Extensions.String;

namespace Apps.AmazonPolly.Extensions;

public static class VoiceExtensions
{
    public static string Describe(this Voice voice)
        => $"{voice.Id} ({voice.LanguageCode}; {string.Join(", ", voice.SupportedEngines.Select(x => x.ToPascalCase()))})";
}